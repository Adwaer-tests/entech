# entech
Test assestment from https://drive.google.com/file/d/1AWAHMFnj_fctRdctAbblZ4Wj46upCD1B/view

## Overview
There are two parts:
1. Create a small web application for farm management.
2. Create a C# console application.

### 1 Create a small web application
Create a web application for farm management that consists of one page. It will be used only by one
farmer. He/she can list, add, and remove animals in the system. Every animal has only a “Name”
property that should be unique.
The frontend should be written on Angular. The backend is on ASP.NET Core. Use in-memory storage on
the server side.
The source code should be production ready.

The solution is inside [webapp](https://github.com/Adwaer-tests/entech/tree/main/webapp) folder
To start the app move to the folder and run: `docker compose up`

### 2 Create a C# console application
Create a C# console application that accepts a matrix of values 0 and 1. The application should
output only one value into the console – number of areas formed of number 1.
The matrix is presented as a string value where ‘,’ is used as a separator for columns, ‘;’ is used as
a separator for rows. For instance, “1,0,1;0,1,0” string value should be converted to the matrix
[[1,0,1], [0,1,0]].
The maximum size of the matrix is 100x100.
Examples of the input and output:
1. Input: “1,0,1;0,1,0”
Output: 3
2. Input: “1,0,1;1,1,0”
Output: 2
3. Input: “1,1,1,0;0,1,0,0”
Output: 1

Try to keep the source code clean and easy to read.