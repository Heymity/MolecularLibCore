// See https://aka.ms/new-console-template for more information

using MolecularLib.CodeGenerator;

const string basePath = @"D:\Unity\UnityProjects\MolecularLibCore\Assets\Lib\Core\InstantiateHelpers\";

File.WriteAllText(basePath + InstantiateMethodsCodeCreator.MethodsFileName, InstantiateMethodsCodeCreator.CreateMethodsCode());
File.WriteAllText(basePath + InstantiateMethodsCodeCreator.InterfacesFileName, InstantiateMethodsCodeCreator.CreateInterfacesCode());
