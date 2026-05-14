CREATE TABLE Comments (
    CommentID INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(255),
    PageName VARCHAR(255),
    CommentText NVARCHAR(MAX),
    CommentDate DATETIME
);