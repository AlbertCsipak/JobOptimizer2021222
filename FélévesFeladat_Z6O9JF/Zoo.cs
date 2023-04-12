using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FélévesFeladat_Z6O9JF
{
    delegate void ZooDelegate(string var);
    sealed partial class Zoo
    {
        public static event ZooDelegate emptyJobs;
        public static event ZooDelegate freeWorkers;
        public static event ZooDelegate UIevent;
        public static event ZooDelegate ExceptionEvent;
        public static MyLinkedList<Zoo.Jobs> Reader(string name)
        {
            MyLinkedList<Zoo.Jobs> jobList = new MyLinkedList<Zoo.Jobs>();
            StreamReader sr = new StreamReader(name);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (ValidInput(line))
                {
                    string[] tmpString = line.Split('_');
                    switch (tmpString[1])
                    {
                        case "Feeding":
                            Zoo.Jobs.Feeding tmp = new Zoo.Jobs.Feeding(tmpString[0] + ' ' + tmpString[1], int.Parse(tmpString[2]));
                            jobList.ItemInsert(tmp);
                            break;
                        case "Quarantine":
                            Zoo.Jobs.Quarantine tmp1 = new Zoo.Jobs.Quarantine(tmpString[0] + ' ' + tmpString[1], int.Parse(tmpString[2]));
                            jobList.ItemInsert(tmp1);
                            break;
                        case "Promoting":
                            Zoo.Jobs.Promoting tmp2 = new Zoo.Jobs.Promoting(tmpString[0] + ' ' + tmpString[1], int.Parse(tmpString[2]));
                            jobList.ItemInsert(tmp2);
                            break;
                        case "CageCleaning":
                            Zoo.Jobs.CageCleaning tmp4 = new Zoo.Jobs.CageCleaning(tmpString[0] + ' ' + tmpString[1], int.Parse(tmpString[2]));
                            jobList.ItemInsert(tmp4);
                            break;
                    }
                }
            }
            sr.Close();
            return jobList;
        }
        public static MyLinkedList<Zoo.Employees> Reader(string name, MyLinkedList<Zoo.Jobs> jobs)
        {
            //v2
            object[,] skillInfo = SmallestSkillNeeded(jobs);
            ;
            UIevent?.Invoke("");
            MyLinkedList<Zoo.Employees> employeeList = new MyLinkedList<Zoo.Employees>();
            StreamReader sr = new StreamReader(name);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (ValidInput(line))
                {
                    string[] tmpString = line.Split('_');

                    switch (tmpString[1])
                    {
                        case "CareTaker":
                            try
                            {
                                if (int.Parse(tmpString[2]) < Convert.ToInt32(skillInfo[1, 1]))
                                {
                                    throw new Exceptions.NotQualifiedException(tmpString[0] + " (" + tmpString[2] + ")", tmpString[1] + " (" + skillInfo[1, 1] + ")");
                                }
                            }
                            catch (Exception b)
                            {
                                Training(ref tmpString[2], skillInfo[1, 1]);
                                ExceptionEvent?.Invoke(b.Message);
                            }
                            Zoo.Employees.CareTaker tmp = new Zoo.Employees.CareTaker(tmpString[0], tmpString[1], int.Parse(tmpString[2]));
                            employeeList.ItemInsert(tmp);
                            break;
                        case "Vet":
                            try
                            {
                                if (int.Parse(tmpString[2]) < Convert.ToInt32(skillInfo[3, 1]))
                                {
                                    throw new Exceptions.NotQualifiedException(tmpString[0] + " (" + tmpString[2] + ")", tmpString[1] + " (" + skillInfo[3, 1] + ")");
                                }
                            }
                            catch (Exception b)
                            {
                                ;
                                Training(ref tmpString[2], skillInfo[3, 1]);
                                ExceptionEvent?.Invoke(b.Message);
                            }
                            ;
                            Zoo.Employees.Vet tmp1 = new Zoo.Employees.Vet(tmpString[0], tmpString[1], int.Parse(tmpString[2]));
                            employeeList.ItemInsert(tmp1);
                            break;
                        case "Cashier":
                            try
                            {
                                if (int.Parse(tmpString[2]) < Convert.ToInt32(skillInfo[2, 1]))
                                {
                                    throw new Exceptions.NotQualifiedException(tmpString[0] + " (" + tmpString[2] + ")", tmpString[1] + " (" + skillInfo[2, 1] + ")");
                                }
                            }
                            catch (Exception b)
                            {
                                Training(ref tmpString[2], skillInfo[2, 1]);
                                ExceptionEvent?.Invoke(b.Message);
                            }
                            Zoo.Employees.Cashier tmp2 = new Zoo.Employees.Cashier(tmpString[0], tmpString[1], int.Parse(tmpString[2]));
                            employeeList.ItemInsert(tmp2);
                            break;
                        case "Janitor":
                            try
                            {
                                if (int.Parse(tmpString[2]) < Convert.ToInt32(skillInfo[0, 1]))
                                {
                                    throw new Exceptions.NotQualifiedException(tmpString[0] + " (" + tmpString[2] + ")", tmpString[1] + " (" + skillInfo[0, 1] + ")");
                                }
                            }
                            catch (Exception b)
                            {
                                Training(ref tmpString[2], skillInfo[0, 1]);
                                ExceptionEvent?.Invoke(b.Message);
                            }
                            Zoo.Employees.Janitor tmp3 = new Zoo.Employees.Janitor(tmpString[0], tmpString[1], int.Parse(tmpString[2]));
                            employeeList.ItemInsert(tmp3);
                            break;
                    }
                }
            }
            sr.Close();
            return employeeList;
        }
        static object[,] SmallestSkillNeeded(MyLinkedList<Zoo.Jobs> jobVar)
        {
            object[,] array = new object[4, 2];

            array[0, 0] = "CageCleaning";
            array[0, 1] = 10;

            array[1, 0] = "Feeding";
            array[1, 1] = 10;

            array[2, 0] = "Promoting";
            array[2, 1] = 10;

            array[3, 0] = "Quarantine";
            array[3, 1] = 10;

            foreach (var item in jobVar)
            {
                if (item is Zoo.Jobs.CageCleaning && item.Priority < Convert.ToInt32(array[0, 1]))
                {
                    array[0, 1] = item.Priority;
                }
                else if (item is Zoo.Jobs.Feeding && item.Priority < Convert.ToInt32(array[1, 1]))
                {
                    array[1, 1] = item.Priority;
                }
                else if (item is Zoo.Jobs.Promoting && item.Priority < Convert.ToInt32(array[2, 1]))
                {
                    array[2, 1] = item.Priority;
                }
                else if (item is Zoo.Jobs.Quarantine && item.Priority < Convert.ToInt32(array[3, 1]))
                {
                    array[3, 1] = item.Priority;
                }
            }
            return array;
        }
        static string Training(ref string skill, object source)
        {
            skill = Convert.ToString(source);
            return skill;
        }
        public static void JobGiver(MyLinkedList<Zoo.Jobs> jobVar, MyLinkedList<Zoo.Employees> employeeVar)
        {
            //v5Final
            string[,] array = new string[jobVar.Count(), 2];

            for (int i = 0; i < jobVar.Count(); i++)
            {
                array[i, 0] = jobVar.ElementAt(i).ToString();
                if (jobVar.ElementAt(i) is Zoo.Jobs.CageCleaning)
                {
                    int j = 0;
                    bool exit = false;
                    while (exit is false && j < employeeVar.Count())
                    {
                        if (employeeVar.ElementAt(j).JobCounter < 5 &&
                            employeeVar.ElementAt(j) is Zoo.Employees.Janitor &&
                            employeeVar.ElementAt(j).Skill >= jobVar.ElementAt(i).Priority)
                        {
                            array[i, 1] = employeeVar.ElementAt(j).ToString();
                            employeeVar.ElementAt(j).JobCounter++;
                            employeeVar.ElementAt(j).JobList.Add(jobVar.ElementAt(i));
                            exit = true;
                        }
                        else
                        {
                            j++;
                        }
                    }
                }
                else if (jobVar.ElementAt(i) is Zoo.Jobs.Feeding)
                {
                    int j = 0;
                    bool exit = false;
                    while (exit is false && j < employeeVar.Count())
                    {
                        if (employeeVar.ElementAt(j).JobCounter < 5 &&
                            employeeVar.ElementAt(j) is Zoo.Employees.CareTaker &&
                            employeeVar.ElementAt(j).Skill >= jobVar.ElementAt(i).Priority)
                        {
                            array[i, 1] = employeeVar.ElementAt(j).ToString();
                            employeeVar.ElementAt(j).JobCounter++;
                            employeeVar.ElementAt(j).JobList.Add(jobVar.ElementAt(i));
                            exit = true;
                        }
                        else 
                        {
                            j++;
                        }
                    }
                }
                else if (jobVar.ElementAt(i) is Zoo.Jobs.Promoting)
                {
                    int j = 0;
                    bool exit = false;
                    while (exit is false && j < employeeVar.Count())
                    {
                        if (employeeVar.ElementAt(j).JobCounter < 5 &&
                            employeeVar.ElementAt(j) is Zoo.Employees.Cashier &&
                            employeeVar.ElementAt(j).Skill >= jobVar.ElementAt(i).Priority)
                        {
                            array[i, 1] = employeeVar.ElementAt(j).ToString();
                            employeeVar.ElementAt(j).JobCounter++;
                            employeeVar.ElementAt(j).JobList.Add(jobVar.ElementAt(i));
                            exit = true;
                        }
                        else
                        {
                            j++;
                        }
                    }
                }
                else if (jobVar.ElementAt(i) is Zoo.Jobs.Quarantine)
                {
                    int j = 0;
                    bool exit = false;
                    while (exit is false && j < employeeVar.Count())
                    {
                        if (employeeVar.ElementAt(j).JobCounter < 5 &&
                            employeeVar.ElementAt(j) is Zoo.Employees.Vet &&
                            employeeVar.ElementAt(j).Skill >= jobVar.ElementAt(i).Priority)
                        {
                            array[i, 1] = employeeVar.ElementAt(j).ToString();
                            employeeVar.ElementAt(j).JobCounter++;
                            employeeVar.ElementAt(j).JobList.Add(jobVar.ElementAt(i));
                            exit = true;
                        }
                        else
                        {
                            j++;
                        }
                    }
                }
            }
            UIevent?.Invoke("");
            List<string> helper = new List<string>();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (array[i, 1] is null)
                {
                    helper.Add(array[i, 0]);
                }
                else
                {
                    emptyJobs?.Invoke($"{array[i, 0]} got assigned to {array[i, 1]}.");
                }
            }
            UIevent?.Invoke("");
            foreach (var item in helper)
            {
                emptyJobs?.Invoke($"{item} could not be assigned because no one is qualified to do it.");
            }
            helper = new List<string>();
            foreach (var item in employeeVar)
            {
                if (item.JobCounter < 5)
                {
                    freeWorkers?.Invoke($"{item.ToString()} can do {5 - item.JobCounter} more task(s).");
                }
                else
                {
                    helper.Add(item.ToString());
                }
            }
            UIevent?.Invoke("");
            foreach (var item in helper)
            {
                freeWorkers?.Invoke($"{item} is busy for the day.");
            }
            ;
        }
        public static void UI(string tasks, string employees)
        {
            UIevent?.Invoke("Welcome!" +
                "\nType 'J' to add Jobs" +
                "\nType 'E' to add Employees" +
                "\nType 'Run' to run the Program" +
                "\nFeel free to type these anytime during the runtime!\n");

            string input = "";
            bool terminalStop = false;

            while (input.ToUpper() != "E" && input.ToUpper() != "J" && input.ToUpper() != "RUN")
            {
                input = Program.UIConsoleInput();
                if (input.ToUpper() == "RUN")
                {
                    terminalStop = true;
                }
            }
            while (terminalStop is false)
            {
                if (input.ToUpper() == "J")
                {
                    bool stop = false;
                    UIevent?.Invoke("\nAdd your desired Tasks in the following form:" +
                        " Name_Feeding/Quarantine/Promoting/CageCleaning_1-9:\n(please don't use spaces)");
                    while (stop is false)
                    {
                        input = Program.UIConsoleInput();
                        if (input.ToUpper() is "E")
                        {
                            stop = true;
                        }
                        else if (input.ToUpper() is "RUN")
                        {
                            stop = true;
                            terminalStop = true;
                        }
                        else if (ValidInput(input))
                        {
                            using (StreamWriter sw = File.AppendText(tasks))
                            {
                                sw.WriteLine(input);
                            }
                        }
                    }
                }
                if (input.ToUpper() == "E")
                {
                    bool stop = false;
                    UIevent?.Invoke("\nAdd your desired Employees in the following form:" +
                        " Name_Vet/CareTaker/Cashier/Janitor_0-9:\n(please don't use spaces)");
                    while (stop is false)
                    {
                        input = Program.UIConsoleInput();
                        if (input.ToUpper() is "J")
                        {
                            stop = true;
                        }
                        else if (input.ToUpper() is "RUN")
                        {
                            stop = true;
                            terminalStop = true;
                        }
                        else if (ValidInput(input))
                        {
                            using (StreamWriter sw = File.AppendText(employees))
                            {
                                sw.WriteLine(input);
                            }
                        }
                    }
                }
            }
        }
        static bool ValidInput(string input)
        {
            if (input.Contains(' '))
            {
                return false;
            }
            else
            {
                string[] tmpString = input.Split('_');
                if (tmpString.Length != 3 || tmpString[2].Contains('-') || int.Parse(tmpString[2]) > 9)
                {
                    return false;
                }
                else
                {
                    if (tmpString[1] == "Feeding" ||
                        tmpString[1] == "Promoting" ||
                        tmpString[1] == "CageCleaning" ||
                        tmpString[1] == "Quarantine" ||
                        tmpString[1] == "Vet" ||
                        tmpString[1] == "Janitor" ||
                        tmpString[1] == "CareTaker" ||
                        tmpString[1] == "Cashier")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

        }
    }
}
