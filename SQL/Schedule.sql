-- procedure
    -- query
    -- return current courses that the student is participating in

create procedure GetCurrentCourses @StudentId UNIQUEIDENTIFIER
AS
    BEGIN
        select [id],[coursename],[lecture_id] as LectureId,[course_code] as Coursecode,[begin_date] as BeginDate,[end_date] as EndDate,[session] as [Session],[date_of_week] as DateOfWeek from [dbo].[course]
            join [dbo].[course_user] on 
            [course_user].[course_ref] = [course].[id]
        where
            begin_date < getdate()
            AND
            end_date > getdate()
            AND
            user_ref = @StudentId
    END
GO

drop procedure GetCurrentCourses

-- test procedure

EXEC GetCurrentCourses @StudentId = 'c2c3f5ad-03aa-4691-b779-268454132de5'
