using System.Text.RegularExpressions;

namespace ClassLibrary
{
    public class Assignment48
    {
        //RunValidation gets called by the main method in the Program.cs
        public void RunValidation()
        {
            //base functionality to add testpapers is implemented
            //user auth needs implementing to prevent students from creating tests and taking tests for each other

            //TODO 1: refactoring to eliminate Factory class and integrate methods into respective classes 
            //Todo 2: expand testTaker to allow students to take other tests

            Console.WriteLine("Test Generator 0.3");

            //get students and tests, and tests per student
            Factory factory = new Factory();
            List<ITestPaper> testPapers = factory.GetTestPapers(factory.GetNumberOfTestPapers());
            string[] studentNames = factory.GetStudentNames(factory.GetNumberOfStudents());
            List<IStudent> students = factory.MakeStudentObjects(studentNames);
            foreach (IStudent student in students)
            {
                student.TestPapers = factory.GetStudentTestPapers(testPapers, student.StudentName);
            }

            //debug logging to see tests, students and tests per student
            //factory.DebugLogging(testPapers, students);

            Console.Clear();
            Console.WriteLine("Test Taker 0.2");
            //user auth needed to prevent students from taking each other's tests

            factory.SelectStudentAndTakeTest(students);
        }
    }
    public interface ITestPaper
    {
        string SubjectName { get; set; }
        string TestPaperName { get; set; }
        List<IQuestion> Questions { get; set; }
        bool TestTaken { get; set; }
        int Score { get; set; }
    }

    public interface IQuestion
    {
        public string QuestionText { get; set; }
        public List<IOption> Options { get; set; }
        public char CorrectAnswerLetter { get; set; }
        public char OptionSelectedByStudent { get; set; }
        public int MarksSecured { get; set; }
    }

    public interface IOption
    {
        public char OptionLetter { get; set; }
        public string OptionText { get; set; }
    }

    public interface IStudent
    {
        public int RollNo { get; set; }
        public string StudentName { get; set; }
        public List<ITestPaper> TestPapers { get; set; }
    }


    public class TestPaper : ITestPaper
    {
        public string SubjectName { get; set; }
        public string TestPaperName { get; set; }
        public List<IQuestion> Questions { get; set; }
        public bool TestTaken { get; set; }
        public int Score { get; set; }

        public TestPaper(string subjectName, string testPaperName, List<IQuestion> questions)
        {
            SubjectName = subjectName;
            TestPaperName = testPaperName;
            Questions = questions;
            TestTaken = false;
            Score = 0;
        }
    }

    public class Question : IQuestion
    {
        public string QuestionText { get; set; }
        public List<IOption> Options { get; set; }
        public char CorrectAnswerLetter { get; set; }
        public char OptionSelectedByStudent { get; set; }
        public int MarksSecured { get; set; }

        public Question(string questionText, List<IOption> options, char correctAnswerLetter)
        {
            QuestionText = questionText;
            Options = options;
            CorrectAnswerLetter = correctAnswerLetter;
            MarksSecured = 0;
        }


    }

    public class Option : IOption
    {
        public char OptionLetter { get; set; }
        public string OptionText { get; set; }

        public Option(char optionLetter, string optionText)
        {
            OptionLetter = optionLetter;
            OptionText = optionText;
        }

    }

    public class Student : IStudent
    {
        public int RollNo { get; set; }
        public string StudentName { get; set; }
        public List<ITestPaper> TestPapers { get; set; }

        public Student(int rollNo, string studentName)
        {
            RollNo = rollNo;
            StudentName = studentName;
            TestPapers = new List<ITestPaper>();
        }
    }

    public class Factory
    {
        Regex regex;

        public int GetNumberOfTestPapers()
        {
            Console.WriteLine("Enter number of Test Papers:");
            int numberOfTestPapers;
            bool validNumberOfTestPapers = int.TryParse(Console.ReadLine(), out numberOfTestPapers);
            while (!validNumberOfTestPapers)
            {
                Console.WriteLine("Invalid Input, enter valid number of tests (numbers only):");
                validNumberOfTestPapers = int.TryParse(Console.ReadLine(), out numberOfTestPapers);
            };
            return numberOfTestPapers;
        }

        public List<ITestPaper> GetTestPapers(int numberOfTestPapers)
        {
            //get questions, options and answer of test paper
            char[] optionLetters = new char[4] { 'A', 'B', 'C', 'D' };
            bool isOptionTextInputValid = false;
            bool isCorrectOptionValid = false;
            List<ITestPaper> testPapers = new List<ITestPaper>();

            for (int i = 0; i < numberOfTestPapers; i++)
            {
                string testPaperSubjectMatter = default;
                string testPaperTitle = default;

                //get subject matter and title
                Console.WriteLine($"Enter subject matter of test paper {i + 1}/{numberOfTestPapers}:");
                testPaperSubjectMatter = Console.ReadLine();
                Console.WriteLine($"Enter title of test paper {i + 1}/{numberOfTestPapers}:");
                testPaperTitle = Console.ReadLine();
                Console.WriteLine($"All tests have 10 questions, and 4 answer options.");


                //get questions
                List<IQuestion> questions = new List<IQuestion>();
                for (int j = 0; j < 10; j++)
                {
                    List<IOption> options = new List<IOption>();
                    string questionText = "";
                    string optionText = "";
                    char correctOption = '\0';

                    //get question text
                    while (questionText.Length < 4)
                    {
                        Console.WriteLine($"Enter text of question {j + 1}/10 of {testPaperSubjectMatter} test paper (min. 4 characters):");
                        questionText = Console.ReadLine();
                    }

                    //get options texts
                    for (int k = 0; k < optionLetters.Length; k++)
                    {
                        regex = new Regex("^[a-zA-Z1-9]{4,}");

                        while (!isOptionTextInputValid)
                        {
                            Console.WriteLine($"Enter text of answer option {optionLetters[k]} (min. 4 characters):");
                            optionText = Console.ReadLine();
                            isOptionTextInputValid = regex.IsMatch(optionText);
                        }
                        Option option = new Option(optionLetters[k], optionText);
                        options.Add(option);
                        isOptionTextInputValid = false;
                    }

                    //get correct option
                    regex = new Regex("^[aAbBcCdD]{1}$");
                    string input = "";
                    while (!isCorrectOptionValid)
                    {
                        Console.WriteLine(
                            $"Enter the correct option {optionLetters[0]}-{optionLetters[optionLetters.Length - 1]} for question {j + 1}/10:");
                        input = Console.ReadLine().ToUpper();
                        isCorrectOptionValid = regex.IsMatch(input);
                    }

                    correctOption = char.Parse(input);
                    Question tempNewQuestion = new Question(questionText, options, correctOption);
                    questions.Add(tempNewQuestion);
                    isCorrectOptionValid = false;
                }

                TestPaper tempTestPaper = new TestPaper(testPaperSubjectMatter, testPaperTitle, questions);
                testPapers.Add(tempTestPaper);
            }
            Console.WriteLine($"Debug: number of testPapers: {testPapers.Count}");
            return testPapers;
        }

        public int GetNumberOfStudents()
        {
            //number of students validation
            int numberOfStudents = 0;
            Console.WriteLine("Enter number of students:");
            bool validnNumberOfStudents = false;
            while (!validnNumberOfStudents)
            {
                Console.WriteLine("Invalid Input, enter a number of students (numbers only):");
                validnNumberOfStudents = int.TryParse(Console.ReadLine(), out numberOfStudents);
            };
            return numberOfStudents;
        }

        public string[] GetStudentNames(int numberOfStudents)
        {
            //collect names of students validation
            regex = new Regex("^[a-zA-Z]{2,}$");
            string[] studentNames = new string[numberOfStudents];
            string input = "";
                
            for (int i = 0; i < numberOfStudents; i++)
            {
                Console.WriteLine($"Student names {i + 1}/{numberOfStudents}");
                bool isValid = false;
                while (!isValid)
                {
                    Console.WriteLine($"Enter a valid name for student {i + 1} (min. 2 letters):");
                    input = Console.ReadLine();
                    isValid = regex.IsMatch(input);
                }
                studentNames[i] = input;
            }
            Console.WriteLine($"Debug: number of studentNames: {studentNames.Length}");
            return studentNames;
        }

        public List<IStudent> MakeStudentObjects(string[] studentNames)
        {
            List<IStudent> tempStudents = new List<IStudent>();
            for (int i = 0; i < studentNames.Length; i++)
            {
                Student newStudent = new Student(i + 1, studentNames[i]);
                tempStudents.Add(newStudent);
            }
            Console.WriteLine($"Debug: number of student objects: {tempStudents.Count}");
            return tempStudents;
        }

        public List<ITestPaper> GetStudentTestPapers(List<ITestPaper> testPapers, string studentName)
        {
            List<ITestPaper> tempStudentTestPapers = new List<ITestPaper>();
            regex = new Regex("^[yY]|[nN]");
                
            for (int i = 0; i < testPapers.Count; i++)
            {
                bool inputValid = false;
                string input = "";
                while (!inputValid)
                {
                    Console.WriteLine($"Is {studentName} taking {testPapers[i].SubjectName} {testPapers[i].TestPaperName}?");
                    Console.WriteLine($"(Y)es or (N)o?");
                    input = Console.ReadLine();
                    inputValid = regex.IsMatch(input);
                }

                if (input.ToUpper() == "y" || input.ToUpper() == "Y")
                {
                    tempStudentTestPapers.Add(testPapers[i]);
                }
            }
            Console.WriteLine($"Debug: number of student {studentName} testpapers: {tempStudentTestPapers.Count}");
            return tempStudentTestPapers;
        }

        public void DebugLogging(List<ITestPaper> testPapers, List<IStudent> students)
        {
            foreach (ITestPaper test in testPapers)
            {
                Console.WriteLine($"{test.SubjectName}, {test.TestPaperName}");
                foreach (IQuestion question in test.Questions)
                {
                    Console.WriteLine($"{question.QuestionText}");
                    foreach (IOption option in question.Options)
                    {
                        Console.WriteLine($"{option.OptionLetter}. {option.OptionText}");
                    }
                    Console.WriteLine($"Correct answer: {question.CorrectAnswerLetter}");
                }
            }

            foreach (IStudent student in students)
            {
                foreach (ITestPaper test in student.TestPapers)
                {
                    if (student.TestPapers.Count != 0)
                    {
                        Console.WriteLine($"{student.StudentName} is due to take {test.SubjectName}, {test.TestPaperName}.");
                    }
                }
            }
        }

        public void SelectStudentAndTakeTest(List<IStudent> students)
        {
            IStudent studentSelected;
            string input = "";
            bool inputValid = false;
            Console.WriteLine("Note: only students due to take tests are being displayed.");

            //display student names due to take tests
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].TestPapers.Count > 0)
                {
                    Console.WriteLine($"{students[i].RollNo}. {students[i].StudentName}");
                }
            }

            //check student selected to take test
            while (!inputValid)
            {
                Console.WriteLine("Select roll number for student to take test (example: 1):");
                input = Console.ReadLine();
                try
                {
                    studentSelected = students.Find(student => student.RollNo == int.Parse(input));

                    //select test to take
                    Console.WriteLine($"Select number of test for student {studentSelected.StudentName} to take:");
                    for (int i = 0; i < studentSelected.TestPapers.Count; i++)
                    {
                        Console.WriteLine($"{i}. {studentSelected.TestPapers[i].SubjectName}, {studentSelected.TestPapers[i].TestPaperName}");
                    }
                    input = Console.ReadLine();

                    if (studentSelected.TestPapers[int.Parse(input)].TestTaken != true)
                    {
                        ITestPaper testSelected = studentSelected.TestPapers[int.Parse(input)];
                        inputValid = true;

                        //process question answers of test for student
                        Regex regex = new Regex("^[aAbBcCdD]{1}$");
                        for (int i = 0; i < testSelected.Questions.Count; i++)
                        {
                            Console.WriteLine($"{testSelected.Questions[i].QuestionText}");
                            Console.WriteLine("Select the letter that answers the question:");
                            for (int j = 0; j < testSelected.Questions[i].Options.Count; j++)
                            {
                                Console.WriteLine($"{testSelected.Questions[i].Options[j].OptionLetter}. {testSelected.Questions[i].Options[j].OptionText}");
                            }
                            input = Console.ReadLine();

                            //if answer is correct, tally up MarksSecured
                            if (regex.IsMatch(input))
                            {
                                if (char.Parse(input) == testSelected.Questions[i].CorrectAnswerLetter)
                                {
                                    testSelected.Questions[i].MarksSecured++;
                                }
                            }
                        }

                        //close off test, tally up score, display score of test
                        testSelected.TestTaken = true;
                        int score = 0;
                        foreach (IQuestion question in testSelected.Questions)
                        {
                            if (question.MarksSecured != 0)
                            {
                                score++;
                            }
                        }
                        testSelected.Score = score;
                        Console.WriteLine($"{testSelected.SubjectName} {testSelected.TestPaperName} completed. Score: {testSelected.Score}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid test selected, already taken or not listed.");
                    }
                }
                catch
                {
                    Console.WriteLine("invalid selection, type the Roll number that identifies a student listed.");
                    inputValid = false;
                }
            }
        }
    }
}
