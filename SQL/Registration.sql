select * from [dbo].[course]
select * from [dbo].[users]
-- status
    -- 0. Pending
    -- 1. Finished
-- course registration database
create table CourseRegistration 
(
    Student UNIQUEIDENTIFIER NOT NULL foreign key REFERENCES [dbo].[users](id),
    Course UNIQUEIDENTIFIER NOT NULL foreign key REFERENCES [dbo].[course](id),
    RegistrationStatus TINYINT
)

drop table [dbo].[CourseRegistration]
go

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
go


-- PROCEDURES
-- procedure for dinding any duplicated course in a JSON request containing student registration information
    -- query
    -- return invalid courses (duplicated in date of week and session)
    -- t1.student,t1.courseId as id,t1.begin_date,t1.end_date,t1.[session],t1.[date_of_week]

create PROCEDURE FindDuplicatedSchedule @Json NVARCHAR(max)
as 
begin
    select [id] as Id,[coursename] as Coursename,[lecture_id] as LecturerId,[course_code] as Coursecode,t1.[begin_date] as BeginDate,t1.[end_date] as EndDate,t1.[session] as [Session],t1.[date_of_week] as [DateOfWeek] from (
        select *  from (select courseId,student from (select * from openjson(@Json) with (
        student UNIQUEIDENTIFIER '$.Student',
        courses NVARCHAR(max) '$.Courses' as json
        ) j1
        outer apply openjson(j1.courses) with (
            courseId UNIQUEIDENTIFIER '$'
        )  
        ) as JsonResult
        union 
        (
            select Course as courseId, Student as student from CourseRegistration
            where Student = JSON_VALUE(@Json,'$.Student')
        )) as j2
    join course on j2.courseId = course.id) 
    as t1
    join 
    (
        select [session],[date_of_week], count(*) as qty from 
            (select student,j2.courseId,begin_date,course.end_date,course.[session],course.date_of_week  from (select courseId,student from (select * from openjson(@Json) with (
            student UNIQUEIDENTIFIER '$.Student',
            courses NVARCHAR(max) '$.Courses' as json
            ) j1
            outer apply openjson(j1.courses) with (
                courseId UNIQUEIDENTIFIER '$'
            )  
            ) as JsonResult
            union 
            (
                select Course as courseId, Student as student from CourseRegistration
                where Student = JSON_VALUE(@Json,'$.Student')
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
    -- command
create procedure RegisterCourse @Json NVARCHAR(max)
AS
    BEGIN
        insert into [dbo].[CourseRegistration]
        select distinct [student] as Student,[courseId] as Course, 0 as [RegistrationStatus] from openjson(@Json) with 
                (
                student UNIQUEIDENTIFIER '$.Student',
                courses NVARCHAR(max) '$.Courses' as json
                ) j1
                outer apply openjson(j1.courses) with (
                    courseId UNIQUEIDENTIFIER '$'
                )

    END
go

drop procedure RegisterCourse
go

-- procedure for delete registration using json
    -- command
create procedure DeleteRegistration @Json NVARCHAR(max)
as
    BEGIN
        delete from [dbo].[CourseRegistration] where 
        Student = JSON_VALUE(@Json,'$.Student')
        and Course in (
            select Course from openjson(@Json,'$.Courses') with (
            Course UNIQUEIDENTIFIER '$'
        )
    )
    END
go

drop procedure DeleteRegistration
go

-- Procedure for add a course to register, this procedure is called when a new course is created
    -- command
create procedure AddAvailableCourse @Id UNIQUEIDENTIFIER
as 
    begin
        insert into [dbo].[AvailableCourse] (Course,CourseStatus) values
        (
            @Id,
            0
        )
    end
go

drop procedure AddAvailableCourse
go
-- Procedure for add courses using JSON
create procedure AddAvailableCourses @Json nvarchar(max)
AS
    begin
        insert into [dbo].[AvailableCourse] (Course,CourseStatus)
            select Course,0 as CourseStatus from openjson(@Json)
            with (
                Course UNIQUEIDENTIFIER '$.Id'
            )
    END
go

drop procedure AddAvailableCourses
go
-- Procedure for Lock Particular course (make it not visible to Register)
    -- command
create procedure LockAvailableCourse @Id UNIQUEIDENTIFIER
as 
    BEGIN 
        update [dbo].[AvailableCourse]
        SET CourseStatus = 1
        WHERE Course = @Id
    END
go

drop procedure LockAvailableCourse
go

-- Procedure for lock all available course, This procedure is called when finish course registering
    -- command
create procedure LockAllCurrentAvailableCourse
as BEGIN
    update [dbo].[AvailableCourse]
    SET CourseStatus = 1
END
go

drop procedure LockAllCurrentAvailableCourse
go

-- Procedure to get available course for registration
    -- query
    -- return courses that is available for registration
create procedure GetAvailableCourses
as
    begin
        select [id],[coursename],[lecture_id] as LecturerId,[course_code] as coursecode,[begin_date] as BeginDate,[end_date] as EndDate,[session],[date_of_week] as DateOfWeek from [dbo].[AvailableCourse]
            join [dbo].[course]
            on [AvailableCourse].[Course] = [Course].[id]
            where CourseStatus = 0
    END
go

drop procedure GetAvailableCourses
go

-- procedure for transfer all registration into course_user
    -- command
create procedure TransferAllRegisteredCourse
as
    begin
        insert into [dbo].[course_user] (user_ref,course_ref)
        select distinct Student as user_ref ,Course as course_ref from [dbo].[CourseRegistration]
            where RegistrationStatus = 0
        update [dbo].[CourseRegistration] 
        set RegistrationStatus = 1
    end 
go

drop procedure TransferAllRegisteredCourse
go


-- procedure for get all current registration of a student
create procedure GetRegistrationRecords @Id UNIQUEIDENTIFIER
AS
select  [id],[coursename],[lecture_id] as LectureId,[course_code] as Coursecode,[begin_date] as BeginDate,[end_date] as EndDate,[session] as [Session],[date_of_week] as DateOfWeek from [dbo].[CourseRegistration] t1
JOIN [dbo].[course] t2 on t1.Course = t2.id
where Student = @Id
GO

drop procedure GetRegistrationRecords
go

-- procedure for start a new registration period



-- procedure for end registration period
    -- lock registration timeline
    -- lock all available course
    -- transfer all current registration course to course_user
CREATE procedure FinializeRegistration
as
    BEGIN
        EXEC FinishRegistrationTimeline
        EXEC LockAllCurrentAvailableCourse
        EXEC TransferAllRegisteredCourse
    END
GO

drop procedure FinializeRegistration




-- TESTING PROCEDURES
-- test check duplicated 
declare @Json NVARCHAR(max);
set @Json = N'
{
    "student":"c2c3f5ad-03aa-4691-b779-268454132de5",
    "courses":[
          "60e63f23-a73c-41ac-b669-acda565413dd"
    ]
}'

declare @temp NVARCHAR(maX)
set @temp = N'{"Student":"9dfe73b8-28a9-4436-857f-d063f9f55a19","Courses":["c9333905-6ab8-4318-bea5-7bfe00f070ee","b63f3f0d-ea95-46c7-b446-977d7d9ed402","60e63f23-a73c-41ac-b669-acda565413dd"]}'

EXEC FindDuplicatedSchedule @Json=@temp
select * from course

-- test Register
declare @test NVARCHAR(max);
set @test = N'
{
    "Student":"c2c3f5ad-03aa-4691-b779-268454132de5",
    "Courses":[
        "c9333905-6ab8-4318-bea5-7bfe00f070ee",
         "b63f3f0d-ea95-46c7-b446-977d7d9ed402",
        "60e63f23-a73c-41ac-b669-acda565413dd"
    ]
}'
Exec RegisterCourse @Json = @test

select * from CourseRegistration

-- test delete Registration
declare @test NVARCHAR(max);
set @test = N'
{
    "Student":"c2c3f5ad-03aa-4691-b779-268454132de5",
    "Courses":[
        "c9333905-6ab8-4318-bea5-7bfe00f070ee",
        "b63f3f0d-ea95-46c7-b446-977d7d9ed402",
        "60e63f23-a73c-41ac-b669-acda565413dd"
    ]
}'
EXEC DeleteRegistration @Json=@test

-- Test add available course
EXEC AddAvailableCourse @CourseId ='60e63f23-a73c-41ac-b669-acda565413dd'

-- Test add available courses
declare @test nvarchar(max)
set @test = N'[
    {
        "Id":"e1397a21-d2e1-4fa4-b1ac-e547cb98d34f",
        "Coursename":"Test Inserted Course 1",
        "LectureId":"1fafd4f4-434f-4810-a6df-e234a78e6171",
        "Coursecode":"TIC01",
        "BeginDate":"2023-01-16",
        "EndDate":"2023-01-23",
        "Session":"1",
        "DateOfWeek":"1"
    },
    {
        "Id":"370f3a2c-135f-4d0f-937e-3ef8aa90c01d",
        "Coursename":"Test Inserted Course 2",
        "LectureId":"1fafd4f4-434f-4810-a6df-e234a78e6171",
        "Coursecode":"TIC02",
        "BeginDate":"2023-01-16",
        "EndDate":"2023-01-23",
        "Session":"0",
        "DateOfWeek":"1"
    },
    {
        "Id":"a33c75b3-d1aa-4600-b5ed-c5920ab3c1ef",
        "Coursename":"Test Inserted Course 3",
        "LectureId":"1fafd4f4-434f-4810-a6df-e234a78e6171",
        "Coursecode":"TIC03",
        "BeginDate":"2023-01-16",
        "EndDate":"2023-01-23",
        "Session":"0",
        "DateOfWeek":"0"
    }
]'
EXEC AddAvailableCourses @Json = @test

-- Test lock all course
EXEC LockAllCurrentAvailableCourse

-- Test get available course for registration
EXEC GetAvailableCourses

-- test get current registration records
EXEC GetRegistrationRecords @Id = 'c2c3f5ad-03aa-4691-b779-268454132de5'

-- test finalize registration
EXEC TransferAllRegisteredCourse

select * from [dbo].[course_user]
select * from [dbo].[CourseRegistration]







