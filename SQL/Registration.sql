select * from [dbo].[course]
select * from [dbo].[users]
-- status
    -- 0. Pending
    -- 1. Finished
-- course registration database
create table CourseRegistration 
(
    Student UNIQUEIDENTIFIER foreign key REFERENCES [dbo].[users](id),
    Course UNIQUEIDENTIFIER foreign key REFERENCES [dbo].[course](id),
    RegistrationStatus TINYINT
)
-- add sample course with session and date
insert into [dbo].[course] ([id],[coursename],[course_code],[begin_date],[end_date],[session],[date_of_week]) VALUES 
(
    'c9333905-6ab8-4318-bea5-7bfe00f070ee',
    'Test Course 1',
    'TEST1',
    '2023-01-16',
    '2023-01-23',
    '1',
    '1'
),
(
    '60e63f23-a73c-41ac-b669-acda565413dd',
    'Test Duplicated Course with Course 1',
    'TEST1D',
    '2023-01-16',
    '2023-01-23',
    '1',
    '1'
),
(
    'b63f3f0d-ea95-46c7-b446-977d7d9ed402',
    'Test Course 2',
    'TEST2',
    '2023-01-16',
    '2023-01-23',
    '1',
    '2'
),
(
    '9594515d-69e4-4252-a323-d5166a6bbae9',
    'Test Course 3',
    'TEST3',
    '2023-01-16',
    '2023-01-23',
    '0',
    '2'
)
-- delete test values
delete from [dbo].[course] where id = 'c9333905-6ab8-4318-bea5-7bfe00f070ee'
delete from [dbo].[course] where id = '60e63f23-a73c-41ac-b669-acda565413dd'
delete from [dbo].[course] where id = 'b63f3f0d-ea95-46c7-b446-977d7d9ed402'
delete from [dbo].[course] where id = '9594515d-69e4-4252-a323-d5166a6bbae9'

-- select course
select * from course

-- check registration valid with json

declare @test NVARCHAR(max);
set @test = N'
{
    "student":"c2c3f5ad-03aa-4691-b779-268454132de5",
    "courses":[
        "c9333905-6ab8-4318-bea5-7bfe00f070ee",
        "b63f3f0d-ea95-46c7-b446-977d7d9ed402",
        "60e63f23-a73c-41ac-b669-acda565413dd"
    ]
}'
EXEC FindDuplicatedSchedule @json=@test


GO

create PROCEDURE FindDuplicatedSchedule @json NVARCHAR(max)
as 
begin
    select t1.student,t1.courseId,t1.begin_date,t1.end_date,t1.[session],t1.[date_of_week] from (select j1.student,j2.courseId,begin_date,course.end_date,course.[session],course.date_of_week from openjson(@json) with (
        student UNIQUEIDENTIFIER '$.student',
        courses NVARCHAR(max) '$.courses' as json
    ) j1
    outer apply openjson(j1.courses) with (
        courseId UNIQUEIDENTIFIER '$'
    ) j2
    join course on j2.courseId = course.id) as t1
    join 
    (
        select [session],[date_of_week], count(*) as qty from 
            (select j1.student,j2.courseId,begin_date,course.end_date,course.[session],course.date_of_week from openjson(@json) with (
            student UNIQUEIDENTIFIER '$.student',
            courses NVARCHAR(max) '$.courses' as json
            ) j1
            outer apply openjson(j1.courses) with (
                courseId UNIQUEIDENTIFIER '$'
            ) j2
            join course on j2.courseId = course.id) as t3
            group by t3.[session],t3.[date_of_week]
            having count(*) > 1
    ) as t2
    on t1.[session] = t2.[session] and t1.[date_of_week]=t2.[date_of_week]
END
GO










