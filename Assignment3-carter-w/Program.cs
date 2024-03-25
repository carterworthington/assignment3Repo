
// TODO: declare a constant to represent the max size of the values
// and dates arrays. The arrays must be large enough to store
// values for an entire month.
int physicalSize = 31;
int logicalSize = 0;

double minValue = 0.0;
double maxValue = 1000.0;
// TODO: create a double array named 'values', use the max size constant you declared
// above to specify the physical size of the array.
double[] values = new double[physicalSize];

// TODO: create a string array named 'dates', use the max size constant you declared
// above to specify the physical size of the array.
string[] dates = new string[physicalSize];

bool goAgain = true;
  while (goAgain)
  {
    try
    {
      DisplayMainMenu();
      string mainMenuChoice = Prompt("\nEnter a Main Menu Choice: ").ToUpper();
      if (mainMenuChoice == "L")
        logicalSize = LoadFileValuesToMemory(dates, values);
      if (mainMenuChoice == "S")
        SaveMemoryValuesToFile(dates, values, logicalSize);
      if (mainMenuChoice == "D")
        DisplayMemoryValues(dates, values, logicalSize);
      if (mainMenuChoice == "A")
        logicalSize = AddMemoryValues(dates, values, logicalSize);
      if (mainMenuChoice == "E")
        EditMemoryValues(dates, values, logicalSize);
      if (mainMenuChoice == "Q")
      {
        goAgain = false;
        throw new Exception("Bye, hope to see you again.");
      }
      if (mainMenuChoice == "R")
      {
        while (true)
        {
          if (logicalSize == 0)
					  throw new Exception("No entries loaded. Please load a file into memory");
          DisplayAnalysisMenu();
          string analysisMenuChoice = Prompt("\nEnter an Analysis Menu Choice: ").ToUpper();
          if (analysisMenuChoice == "A")
            FindAverageOfValuesInMemory(values, logicalSize);
          if (analysisMenuChoice == "H")
            FindHighestValueInMemory(values, logicalSize);
          if (analysisMenuChoice == "L")
            FindLowestValueInMemory(values, logicalSize);
          if (analysisMenuChoice == "G")
            GraphValuesInMemory(dates, values, logicalSize);
          if (analysisMenuChoice == "R")
            throw new Exception("Returning to Main Menu");
        }
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"{ex.Message}");
    }
  }

void DisplayMainMenu()
{
	Console.WriteLine("\nMain Menu");
	Console.WriteLine("L) Load Values from File to Memory");
	Console.WriteLine("S) Save Values from Memory to File");
	Console.WriteLine("D) Display Values in Memory");
	Console.WriteLine("A) Add Value in Memory");
	Console.WriteLine("E) Edit Value in Memory");
	Console.WriteLine("R) Analysis Menu");
	Console.WriteLine("Q) Quit");
}

void DisplayAnalysisMenu()
{
	Console.WriteLine("\nAnalysis Menu");
	Console.WriteLine("A) Find Average of Values in Memory");
	Console.WriteLine("H) Find Highest Value in Memory");
	Console.WriteLine("L) Find Lowest Value in Memory");
	Console.WriteLine("G) Graph Values in Memory");
	Console.WriteLine("R) Return to Main Menu");
}

string Prompt(string prompt)
{
  //bool inValidInput = true;
  string myString  = "";
  while (true)
  {
    try
    {
      Console.Write(prompt);
      myString = Console.ReadLine().Trim();
      if(string.IsNullOrEmpty(myString))
        throw new Exception($"Empty Input: Please enter something. ");
      break;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }
  return myString;
}

string GetFileName()
{
	string fileName = "";
	do
	{
		fileName = Prompt("Enter file name including .csv or .txt: ");
	} while (string.IsNullOrWhiteSpace(fileName));
	return fileName;
}

int LoadFileValuesToMemory(string[] dates, double[] values)
{
	string fileName = GetFileName();
	int logicalSize = 0;
	string filePath = $"./data/{fileName}";
	if (!File.Exists(filePath))
		throw new Exception($"The file {fileName} does not exist.");
	string[] csvFileInput = File.ReadAllLines(filePath);
	for(int i = 0; i < csvFileInput.Length; i++)
	{
		Console.WriteLine($"lineIndex: {i}; line: {csvFileInput[i]}");
		string[] items = csvFileInput[i].Split(',');
		for(int j = 0; j < items.Length; j++)
		{
			Console.WriteLine($"itemIndex: {j}; item: {items[j]}");
		}
		if(i != 0)
		{
		dates[logicalSize] = items[0];
    values[logicalSize] = double.Parse(items[1]);
    logicalSize++;
		}
	}
  Console.WriteLine($"Load complete. {fileName} has {logicalSize} data entries");
	return logicalSize;
}

void DisplayMemoryValues(string[] dates, double[] values, int logicalSize)
{
	if(logicalSize == 0)
		throw new Exception($"No Entries loaded. Please load a file to memory or add a value in memory");
    Array.Sort(dates, values, 0, logicalSize);
	Console.WriteLine($"\nCurrent Loaded Entries: {logicalSize}");
	Console.WriteLine($"   Date     Value");
	for (int i = 0; i < logicalSize; i++)
		Console.WriteLine($"{dates[i]}   {values[i]}");
}

double PromptDoubleBetweenMinMax(String prompt, double min, double max)
{
  bool inValidInput = true;
  double num = 0;
  while (inValidInput)
  {
    try
    {
      Console.Write($"{prompt} between {min:n2} and {max:n2}: ");
      num = double.Parse(Console.ReadLine());
      if (num < min || num > max)
        throw new Exception($"Invalid. Must be between {min} and {max}. ");
      inValidInput = false;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"{ex.Message}");
    }
  }
  return num;
}

string PromptDate(string prompt)
{
  bool inValidInput = true;
  DateTime date = DateTime.Today;
  Console.WriteLine(date);
  while (inValidInput)
  {
    try
    {
      Console.Write(prompt);
      date = DateTime.Parse(Console.ReadLine());
      Console.WriteLine(date);
      inValidInput = false;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"{ex.Message}");
    }
  }
  return date.ToString("MM-dd-yyyy");
}

double FindHighestValueInMemory(double[] values, int logicalSize)
{
    double highestValue = values[0];
    for (int i = 1; i < values.Length; i++)
    
    if (values[i] > highestValue)
    {
        highestValue = values[i];
    }
	
    
    
    Console.WriteLine($"The Highest Value is: {highestValue}");
    return highestValue;
	
}

double FindLowestValueInMemory(double[] values, int logicalSize)
{
	 double lowestValue = values[0];
    for (int i = 1; i < logicalSize; i++)
    {
        if (values[i] < lowestValue)
        {
            lowestValue = values[i];
        }
    }

    Console.WriteLine($"The Lowest Value is: {lowestValue}");
    return lowestValue;
	
}

void FindAverageOfValuesInMemory(double[] values, int logicalSize)
{
	double sum = 0;
    for (int i = 0; i < logicalSize; i++)
    {
        sum += values[i];
    }

    double average = sum / logicalSize;
    
    Console.WriteLine($"The Average of Values: {average:n1}");
	
}

void SaveMemoryValuesToFile(string[] dates, double[] values, int logicalSize)
{
	Console.WriteLine("Not Implemented Yet");
	//TODO: Replace this code with yours to implement this function.
}

int AddMemoryValues(string[] dates, double[] values, int logicalSize)
{
	double value = 0.0;
  string dateString = "";

  
  dateString = PromptDate("Enter date format mm-dd-yyyy (eg 11-23-2023): ");
  bool found = false;
  for (int i = 0; i < logicalSize; i++)
    if (dates[i].Equals(dateString))
      found = true;
  if(found == true)
    throw new Exception($"{dateString} is already in memory. Edit entry instead.");
  value = PromptDoubleBetweenMinMax($"Enter a double value", minValue, maxValue);
  dates[logicalSize] = dateString;
  values[logicalSize] = value;
  logicalSize++;
  return logicalSize;
}

void EditMemoryValues(string[] dates, double[] values, int logicalSize)
{
	double value = 0.0;
  string dateString = "";
  int foundIndex = 0;

  if(logicalSize == 0)
    throw new Exception($"No Entries loaded. Please load a file to memory or add a value in memory");
  dateString = PromptDate("Enter date format mm-dd-yyyy (eg 11-23-2023): ");
  bool found = false;
  for (int i = 0; i < logicalSize; i++)
    if (dates[i].Equals(dateString))
    {
      found = true;
      foundIndex = i;
    }
  if(found == false)
    throw new Exception($"{dateString} is not in memory. Add entry instead. ");
  value = PromptDoubleBetweenMinMax($"Enter a double value", minValue, maxValue);
  values[foundIndex] = value;
}

void GraphValuesInMemory(string[] dates, double[] values, int logicalSize)
{
  //for (int row = yAxisMaxValue; row >= int.MinValue; row-=yAxisSubtract)
  //Console.Write($"\n{row,yAxisWidth:c0} |");
	//Console.WriteLine("Not Implemented Yet");
  // uses 3 for loops, one for rows, columns and logical size (looks at each data value in our arrays). also a substring. 
	
  
  //TODO: Replace this code with yours to implement this function.
}