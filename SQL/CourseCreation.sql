-- procedure for filter all invalid course in JSON

-- procedure for creating multiple course at once using JSON
create procedure CreateCourses @Json nvarchar(max)
as 
    begin
        insert into [dbo].[course]
            select * from openjson(@Json)
            with
                (
                    id UNIQUEIDENTIFIER '$.Id',
                    coursename nvarchar(max) '$.Coursename',
                    lecture_id UNIQUEIDENTIFIER '$.LectureId',
                    course_code nvarchar(max) '$.Coursecode',
                    begin_date datetime2(7) '$.BeginDate',
                    end_date datetime2(7) '$.EndDate',
                    [session] bit '$.Session',
                    date_of_week int '$.DateOfWeek'
                )
    end
go

drop procedure CreateCourses
go

-- procedure for creating multiple course and then add them into available course database
create procedure CreateAvailableCoursesForRegistration @Json NVARCHAR(max)
AS
    BEGIN
        EXEC CreateCourses @Json = @Json
        EXEC AddAvailableCourses @Json = @Json
    END
go

drop procedure CreateAvailableCoursesForRegistration
-- test create courses

-- limitation of SQL Server is that we cannot return the GUID in insert query, which violates CQRS pattern. The solution is to
    -- generate GUID themselves in backend and then insert as JSON
    -- this is sample data
declare @Json NVARCHAR(max)
set @Json = N'[
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

EXEC CreateCourses @Json=@Json

-- test create courses which are available for registration
declare @Json NVARCHAR(max)
set @Json = N'[
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

EXEC CreateAvailableCoursesForRegistration @Json = @Json


select * from course
select * from [dbo].[AvailableCourse]

delete from [dbo].[AvailableCourse] where Course = 'e1397a21-d2e1-4fa4-b1ac-e547cb98d34f'
delete from [dbo].[AvailableCourse] where Course = '370f3a2c-135f-4d0f-937e-3ef8aa90c01d'
delete from [dbo].[AvailableCourse] where Course = 'a33c75b3-d1aa-4600-b5ed-c5920ab3c1ef'
delete from [dbo].[course] where id = 'e1397a21-d2e1-4fa4-b1ac-e547cb98d34f'
delete from [dbo].[course] where id = '370f3a2c-135f-4d0f-937e-3ef8aa90c01d'
delete from [dbo].[course] where id = 'a33c75b3-d1aa-4600-b5ed-c5920ab3c1ef'




