// ask for input
Console.WriteLine("Enter 1 to create data file.");
Console.WriteLine("Enter 2 to parse data.");
Console.WriteLine("Enter anything else to quit.");
// input response
string? resp = Console.ReadLine();

if (resp == "1")
{
    // TODO: create data file
    // create data file

    // ask a question
    Console.WriteLine("How many weeks of data is needed?");
    // input the response (convert to int)
    int weeks = int.Parse(Console.ReadLine());

    // determine start and end date
    DateTime today = DateTime.Now;
    // we want full weeks sunday - saturday
    DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
    // subtract # of weeks from endDate to get startDate
    DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));

    // random number generator
    Random rnd = new Random();

    // create file
    StreamWriter sw = new StreamWriter("data.txt");

    // loop for the desired # of weeks
    while (dataDate < dataEndDate)
    {
        // 7 days in a week
        int[] hours = new int[7];
        for (int i = 0; i < hours.Length; i++)
        {
            // generate random number of hours slept between 4-12 (inclusive)
            hours[i] = rnd.Next(4, 13);
        }
        // M/d/yyyy,#|#|#|#|#|#|#
        // Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
        sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
        // add 1 week to date
        dataDate = dataDate.AddDays(7);
    }

    sw.Close();
}

else if (resp == "2")
{
    string[] lines = File.ReadAllLines("data.txt");
    foreach (string line in lines)
    {
        string[] parts = line.Split(',');
        DateTime startDate = DateTime.Parse(parts[0]);
        string[] hoursOfSleep = parts[1].Split('|');

        Console.WriteLine("{0}{1}", "Week of ", startDate.ToString("MMM, dd, yyyy"));
        Console.WriteLine("{0,3}{1,3}{2,3}{3,3}{4,3}{5,3}{6,3}{7,4}{8,4}", "Su", "Mo", "Tu", "We", "Th", "Fr", "Sa", "Tot", "Avg");
        Console.WriteLine("{0,3}{1,3}{2,3}{3,3}{4,3}{5,3}{6,3}{7,4}{8,4}", "--", "--", "--", "--", "--", "--", "--", "---", "---");

        int totalHours = 0;
        for (int i = 0; i < hoursOfSleep.Length; i++)
        {
            int hours = int.Parse(hoursOfSleep[i]);
            totalHours += hours;
            Console.Write("{0,3}", hours);
            Console.Write(i < hoursOfSleep.Length - 1 ? "" : $"{totalHours,4}{totalHours / (i + 1),4:F1}\n");
        }
        Console.WriteLine();
    }
}
