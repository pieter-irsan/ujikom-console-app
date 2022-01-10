CREATE DATABASE Praktek
GO


CREATE TABLE ProductionCost
(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    Quantity INT NOT NULL, 
    VariableCost INT NOT NULL, 
    FixedCost INT NOT NULL, 
    TotalCost INT NOT NULL, 
    MarginalCost INT NOT NULL,
    AverageFixedCost INT NOT NULL,
    AverageVariableCost INT NOT NULL,
    AverageTotalCost INT NOT NULL
)
GO


CREATE PROCEDURE InsertCosts
    @Quantity INT NOT NULL,
    @VariableCost INT NOT NULL,
    @FixedCost INT NOT NULL,
    @TotalCost INT NOT NULL,
    @MarginalCost INT NOT NULL,
    @AverageFixedCost INT NOT NULL,
    @AverageVariableCost INT NOT NULL,
    @AverageTotalCost INT NOT NULL
AS
    INSERT INTO ProductionCost
    (
        Quantity, 
        VariableCost, 
        FixedCost, 
        TotalCost, 
        MarginalCost,
        AverageFixedCost,
        AverageVariableCost,
        AverageTotalCost
    )
    VALUES
    (
        @Quantity, 
        @VariableCost, 
        @FixedCost, 
        @TotalCost, 
        @MarginalCost,
        @AverageFixedCost,
        @AverageVariableCost,
        @AverageTotalCost
    )
GO


CREATE PROCEDURE GetPrevQuantityAndTotalCost
AS
SELECT TOP 1 Quantity, TotalCost
FROM ProductionCost
ORDER BY Id DESC
GO


CREATE PROCEDURE GetCosts
AS
SELECT * FROM ProductionCost
GO


CREATE PROCEDURE DeleteRow
    @Quantity INT
AS
    DELETE FROM ProductionCost
    WHERE Quantity = @Quantity
GO


CREATE PROCEDURE ResetProductionCost
AS
TRUNCATE TABLE ProductionCost
GO
