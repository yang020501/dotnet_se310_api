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
select * from CourseRegistration
-- available course table
    -- 0. Available
    -- 1. Closed 
create table AvailableCourse 
(
    Course UNIQUEIDENTIFIER foreign key REFERENCES [dbo].[course](id),
    CourseStatus TINYINT
)
go

select * from [dbo].[AvailableCourse]
go
-- procedures
    -- add available course for registration
    -- call this procedure whenever course creation
create procedure AddAvailableCourse @CourseId UNIQUEIDENTIFIER
as 
    begin
        insert into [dbo].[AvailableCourse] (Course,CourseStatus) values
        (
            @courseId,
            0
        )
    end
go
    -- remove available course from registration
create procedure LockAvailableCourse @CourseId UNIQUEIDENTIFIER
as 
    BEGIN 
        update [dbo].[AvailableCourse]
        SET CourseStatus = 1
        WHERE Course = @CourseId
    END
go
    -- remove ALL available course
    -- call this procedure when finish course registering
create procedure LockAllCurrentAvailableCourse
as BEGIN
    update [dbo].[AvailableCourse]
    SET CourseStatus = 1
END
go
    -- get all available course
create procedure GetAvailableCourses
as
    begin
        select [id],[coursename],[lecture_id],[course_code],[begin_date],[end_date],[session],[date_of_week] from [dbo].[AvailableCourse]
            join [dbo].[course]
            on [AvailableCourse].[Course] = [Course].[id]
            where CourseStatus = 0
    END
go

-- test executing
EXEC AddAvailableCourse @CourseId ='60e63f23-a73c-41ac-b669-acda565413dd'
EXEC LockAllCurrentAvailableCourse
EXEC GetAvailableCourses

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







declare @Json NVARCHAR(max);
set @Json = N'
{
    "student":"c2c3f5ad-03aa-4691-b779-268454132de5",
    "courses":[
          "60e63f23-a73c-41ac-b669-acda565413dd"
    ]
}'
EXEC FindDuplicatedSchedule @Json=@Json

select student,j2.courseId,begin_date,course.end_date,course.[session],course.date_of_week  from (select courseId,student from (select * from openjson(@Json) with (
        student UNIQUEIDENTIFIER '$.student',
        courses NVARCHAR(max) '$.courses' as json
        ) j1
        outer apply openjson(j1.courses) with (
            courseId UNIQUEIDENTIFIER '$'
        )  
        ) as JsonResult
        union 
        (
            select Course as courseId, Student as student from CourseRegistration
            where Student = JSON_VALUE(@Json,'$.student')
        )) as j2
    join course on j2.courseId = course.id  



select distinct * from CourseRegistration


GO
-- procedure for dinding any duplicated course in a JSON request containing student registration information
create PROCEDURE FindDuplicatedSchedule @Json NVARCHAR(max)
as 
begin
    select t1.student,t1.courseId,t1.begin_date,t1.end_date,t1.[session],t1.[date_of_week] from (
        select student,j2.courseId,begin_date,course.end_date,course.[session],course.date_of_week  from (select courseId,student from (select * from openjson(@Json) with (
        student UNIQUEIDENTIFIER '$.student',
        courses NVARCHAR(max) '$.courses' as json
        ) j1
        outer apply openjson(j1.courses) with (
            courseId UNIQUEIDENTIFIER '$'
        )  
        ) as JsonResult
        union 
        (
            select Course as courseId, Student as student from CourseRegistration
            where Student = JSON_VALUE(@Json,'$.student')
        )) as j2
    join course on j2.courseId = course.id) 
    as t1
    join 
    (
        select [session],[date_of_week], count(*) as qty from 
            (select student,j2.courseId,begin_date,course.end_date,course.[session],course.date_of_week  from (select courseId,student from (select * from openjson(@Json) with (
            student UNIQUEIDENTIFIER '$.student',
            courses NVARCHAR(max) '$.courses' as json
            ) j1
            outer apply openjson(j1.courses) with (
                courseId UNIQUEIDENTIFIER '$'
            )  
            ) as JsonResult
            union 
            (
                select Course as courseId, Student as student from CourseRegistration
                where Student = JSON_VALUE(@Json,'$.student')
            )) as j2
            join course on j2.courseId = course.id  
            ) as t3
            group by t3.[session],t3.[date_of_week]
            having count(*) > 1
    ) as t2
    on t1.[session] = t2.[session] and t1.[date_of_week]=t2.[date_of_week]
END
GO

drop procedure FindDuplicatedSchedule
go

-- procedure for register 
create procedure RegisterCourse @Json NVARCHAR(max)
AS
    BEGIN
        insert into [dbo].[CourseRegistration]
        select distinct [student] as Student,[courseId] as Course, 0 as [RegistrationStatus] from openjson(@Json) with 
                (
                student UNIQUEIDENTIFIER '$.student',
                courses NVARCHAR(max) '$.courses' as json
                ) j1
                outer apply openjson(j1.courses) with (
                    courseId UNIQUEIDENTIFIER '$'
                )

    END
go

-- procedure for delete registration using json
create procedure DeleteRegistration @Json NVARCHAR(max)
as
    BEGIN
        delete from [dbo].[CourseRegistration] where 
        Student = JSON_VALUE(@Json,'$.student')
        and Course in (
            select Course from openjson(@Json,'$.courses') with (
            Course UNIQUEIDENTIFIER '$'
        )
    )
    END
go
-- procedure for transfer all registration into course_user




-- test Register
declare @test NVARCHAR(max);
set @test = N'
{
    "student":"c2c3f5ad-03aa-4691-b779-268454132de5",
    "courses":[
        "c9333905-6ab8-4318-bea5-7bfe00f070ee"
    ]
}'
Exec RegisterCourse @Json = @test

select * from CourseRegistration

-- test delete Registration
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
EXEC DeleteRegistration @Json=@test







