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


CREATE PROCEDURE InsertCost
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