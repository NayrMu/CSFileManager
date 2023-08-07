using System;
using System.Reflection;
using SharedDataSpace;

/*
 * 
 */

public class MainPrompt {
    
    SharedData sharedData;
    Dictionary<string, List<string>> commandDic;
    Type? workingCommand;
    Type[] allTypes;
    object? workingCommandInstance;
    MethodInfo? noMod;

    Dictionary<string, Type> allCommands = new Dictionary<string, Type> {
        {"cd", typeof(ChangeDirectory) },
        {"view", typeof(ViewDirectory) },
        {"copy", typeof(Copy) },
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
                List<string> arr1 = commandDic["commandSplit"];
                workingCommand = allCommands[arr1[0]];
                workingCommandInstance = Activator.CreateInstance(workingCommand, sharedData);
                MethodInfo? modifier = workingCommand.GetMethod(commandDic["modSplit"][0]);
                if ( modifier != null ) {
                    if ( modifier.GetParameters().Length > 0 ) {
                        modifier.Invoke(workingCommandInstance, new object[] { commandDic["inputSplit"] });
                    }
                    else {
                        modifier.Invoke(workingCommandInstance, null);
                    }
                }
                else {
                    Console.WriteLine("Modifier not recognized.");
                }
            }
            catch (ArgumentException ex) {
                Console.WriteLine("A syntac error occured: " + ex.Message);
                continue;
            }
            catch (Exception ex) {
                Console.WriteLine("An unexpected error occured: " + ex.Message);
                continue;
            }
            
        }
    }

    Dictionary<string, List<string>> CommandSplitter(string commandStr) {


        commandStr = commandStr.Replace("..", $@"{sharedData.WorkingDir}\");

        List<string> commandSplit = commandStr.Split("|").ToList();
        commandSplit[0] = commandSplit[0].Replace(" ", "");
        List<string> inputSplit = commandSplit[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
        commandSplit.RemoveAt(1);
        commandSplit = string.Join("", commandSplit).Split("-").ToList();
        List<string> modSplit = commandSplit.Skip(0).ToList();
        Console.WriteLine(modSplit[0]);
        Console.WriteLine(modSplit[1]);
        modSplit.RemoveAt(0);
        Console.WriteLine(modSplit[0]);


        
        Dictionary<string, List<string>> returnDic = new Dictionary<string, List<string>> {
            {"commandSplit", commandSplit}, 
            {"inputSplit", inputSplit},
            {"modSplit", modSplit },


        };

        
        

        return returnDic;
    }
}
