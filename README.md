# Quick Setup Guide

## **Setting `DPA.CLI` as the Startup Project**

### **1. Open the Solution**
Launch Visual Studio 2022 and open the project's solution file (`.sln`).

### **2. Set Startup Project**
In Solution Explorer, right-click on `DPA.CLI` and select **Set as Startup Project**.

## Configure to run with Debug command-line arguments

### **1. Configure Debug command-line arguments**
- Right-click on `DPA.CLI` in Solution Explorer and select **Properties**.
- In the properties window, under the **Debug** tab, you'll see a section titled **General**. Locate and click the link for **Open debug launch profiles UI**.
- In the **Debug** launch profiles, specify command-line arguments.

### **2. Run**
Press **F5** or click the **Start** button to run `DPA.CLI` with the specified input parameters.


## **Running `declared-persons-analyser.exe` from the Command Line**

### 1. Build the solution

Go to `Build > Build Solution` (or press `Ctrl+Shift+B`)

If you're running the `DPA.CLI` executable directly from the command line (outside of Visual Studio), ensure the `appsettings.json` file is located in the same folder as the `declared-persons-analyser.exe` file for the application to load its configuration settings correctly.
**Important Note**: When moving the executable, copy the entire contents of the project's `bin\Debug\netX.x` or `bin\Release\netX.x` directory to your desired location. This includes the `.exe` file, any related `.dll` files, and the `appsettings.json` file.


## Console Application command-line arguments of `declared-persons-analyser`

- **`-source`**:  
  The service URL to fetch data from.  
  **Usage**: `-source <URL>`  
  **Example**: `-source https://www.example.com/odata/service`

- **`-district`**:  
  The district Id for which data is to be analyzed. This is required.
  **Usage**: `-district <Id>`  
  **Example**: `-district 123`

- **`-year`**:  
  The year for which data is to be fetched.  
  **Usage**: `-year <YYYY>`  
  **Example**: `-year 2021`

- **`-month`**:  
  The month for which data is to be fetched.  
  **Usage**: `-month <MM>`  
  **Example**: `-month 12`

- **`-day`**:  
  The day for which data is to be fetched.  
  **Usage**: `-day <DD>`  
  **Example**: `-day 31`

- **`-limit`**:  
  Limits the amount of entries returned. If not specified, a default limit is applied.  
  **Usage**: `-limit <number>`  
  **Example**: `-limit 100`

- **`-group`**:  
  Specifies the grouping mode for the output data. Possible values are `y`, `m`, `d`, `ym`, `yd`, `md`.  
  **Usage**: `-group <mode>`  
  **Example**: `-group ym`

- **`-out`**:  
  The name of the output JSON file (including `.json` extension) where the result will be saved.  
  **Usage**: `-out <filename>`  
  **Example**: `-out result.json`

### Example Command

Here's an example of how to run the `declared-persons-analyser` with a set of input parameters:

`declared-persons-analyser.exe -district 516 -year 2019 -limit 4 -group ym -out res.json`
