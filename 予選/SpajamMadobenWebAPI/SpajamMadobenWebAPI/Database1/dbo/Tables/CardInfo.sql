CREATE TABLE [dbo].[CardInfo] (
    [CardID]     NCHAR (6) NULL,
    [CustomerID] NCHAR (5) NULL,
    [IssueDate]  DATETIME  NULL,
    [ExpireDate] DATETIME  NULL,
    [EmployeeID] INT       NULL
);


GO
CREATE CLUSTERED INDEX [CardInfo_Index]
    ON [dbo].[CardInfo]([CardID] ASC);

