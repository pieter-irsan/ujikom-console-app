# Production Costs Calculator
A C# console app to calculate Marginal Cost, Average Fixed Cost, Average Variable Cost, and Average Total Cost.

## Description

The program takes three inputs:
- Fixed Cost `(FC)`
- Variable Cost `(VC)`
- Production Quantity `(Q)`
- Total Cost `(TC)`

  `TC = FC + VC`
<br/>

And produces four outputs:
- Marginal Cost `(MC)`

  `MC = (TC2 - TC1) / (Q2 - Q1)`
- Average Fixed Cost `(AFC)`

  `AFC = FC / Q`
- Average Variable Cost `(AVC)` 

  `AVC = VC / Q`
- Average Total Cost `(ATC)`

  `ATC = TC / Q`

## Installation
To get started with the project, run the `.sql` file to create all the neccessary database tables and procedures. And then type `dotnet run` in your terminal.
