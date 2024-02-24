namespace ClassLibrary
{
    public class Assignment47
    {
        //gets called by the main method in the Program.cs
        public void RunValidation()
        {
            //references and instantiates the lists and adds them to list of lists
            List<List<int>> listOfLists = new List<List<int>>();
            listOfLists.Add(new List<int>() {32, 432, 554, 332, 231, 1011, 4354});
            listOfLists.Add(new List<int>() {55, 78, 775, 4355, 323 });
            listOfLists.Add(new List<int>() {675, 324, 886, 4356 });
            
            //retrieve largest numbers list
            List<int> largestNumbersList = FindLargest(listOfLists);
            
            //prints out each largest numbers
            for (int i = 0; i < largestNumbersList.Count; i++)
            {
                Console.WriteLine($"Largest at index {i}: {largestNumbersList[i]}");
            }
        }

        public List<int> FindLargest(List<List<int>> collections)
        {
            List<int> tempLargestNumbersList = new List<int>();
            
            foreach (List<int> list in collections)
            {
                int largestNumber = 0;
                foreach (int number in list)
                {
                    if (number > largestNumber)
                    {
                        largestNumber = number;
                    }
                }
                tempLargestNumbersList.Add(largestNumber);
            }

            return tempLargestNumbersList;
        }
    }
}