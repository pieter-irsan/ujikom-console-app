# Production Costs Calculator
This is my finished C# console app that was supposed to be submitted for the Professional Programmer Certification Test (Uji Kompetensi Skema Programmer) by LSP UI, but I wasn't able to finish in time back then.

## Description

The program takes four inputs:
- Fixed Cost (FC)
- Variable Cost (VC)
- Total Cost (TC)
- Production Quantity (Q)

And produces four outputs:
- Marginal Cost (MC)

  MC = ΔTC / ΔQ || MC = (TC2 - TC1) / (Q2 - Q1)
- Average Fixed Cost (AFC)

  AFC = ΣFC / Q
- Average Variable Cost (AVC) 

  AVC = ΣVC / Q
- Average Total Cost (ATC)

  ATC = ΣFC + ΣVC

## Installation
To get started with the project, run the `.sql` file to create all the neccessary database tables and procedures. And then type `dotnet run` in your terminal.
