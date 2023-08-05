using System;
using System.Reflection;
using SharedDataSpace;

/*
 * TODO
 * 1. Determine if user gave a mod or not
 * 2. Allow for multiple mods
 * 3. Flesh out commands and mods
 */

public class MainPrompt {
    
    SharedData sharedData;
    Dictionary<string, List<string>> commandDic;
    Type? workingCommand;
    Type[] allTypes;
    object? workingCommandInstance;
    MethodInfo? noMod;

    Dictionary<string, Type> allCommands = new Dictionary<string, Type> {
        {"cd", typeof(ChangeDirectory) }
    };

    public MainPrompt() {
        sharedData = new SharedData();
        allTypes = Assembly.GetExecutingAssembly().GetTypes();
    }

    static void Main() {
        MainPrompt prompt = new MainPrompt();
        prompt.Run();
        
    }
    public void Run() {
        while ( true ) {
            Console.Write($"{sharedData.WorkingDir}:: ");
            try {
                sharedData.CommandInput = Console.ReadLine();
                commandDic = CommandSplitter(sharedData.CommandInput);
                List<string> arr1 = commandDic["arr1"];
                workingCommand = allCommands[arr1[0]];
                workingCommandInstance = Activator.CreateInstance(workingCommand, sharedData);
                MethodInfo? modifier = workingCommand.GetMethod(commandDic["arrMods"][0]);
                if ( modifier != null ) {
                    modifier.Invoke(workingCommandInstance,new object[] { commandDic["arrInputs"] });
                }
                else {
                    Console.WriteLine("Modifier not recognized.");
                }
            }
            catch (ArgumentException ex) {
                Console.WriteLine("An error occured: " + ex.Message);
                continue;
            }
            
        }
    }

    Dictionary<string, List<string>> CommandSplitter(string commandStr) {

        List<string> arr1;
        arr1 = commandStr.Split(" ").ToList();
        List<string> arr2;
        arr2 = arr1[1].Split("|").ToList();
        List<string> arrMods;
        List<string> arrInputs;
        arrMods = arr2[0].Split("-", StringSplitOptions.RemoveEmptyEntries).ToList();
        arrInputs = arr2[1].Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
        Dictionary<string, List<string>> returnDic = new Dictionary<string, List<string>> {
            { "arr1", arr1 },
            { "arrMods", arrMods },
            { "arrInputs", arrInputs }
        };

        return returnDic;
    }
}
