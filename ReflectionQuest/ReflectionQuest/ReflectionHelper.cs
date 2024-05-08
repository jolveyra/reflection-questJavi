using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionQuest
{
    internal class ReflectionHelper
    {
        public MemberInfo GetNonStaticPublicClassMemberMethodWithLargestArgumentList(string assemblyPath)
        {
            Console.WriteLine("Get the name of a class member which is non static nad has the largest argument list in assembly");
            Assembly assembly = Assembly.LoadFrom(assemblyPath);

            Console.WriteLine("Obtener estaticos o publicos");
            assembly.GetTypes().ToList().ForEach(t => Console.WriteLine(t.GetMembers(BindingFlags.Public | BindingFlags.Static)));

            Console.WriteLine("Obtener publicos");
            assembly.GetTypes().ToList().ForEach(t => Console.WriteLine(t.GetMembers(BindingFlags.Public)));

            //BindingFlags metadato sobre miembros clave valor
            //Iterar sobre tipos, iterar sobre miembros, filtrar no estaticos y publicos, 
            //Capaz que para obtener los no estaticos conviene hacer la resta de los estaticos con los publicos
        
            Console.ReadLine();

            throw new NotImplementedException("Not implemented D:");
        }

        public MemberInfo FindMethodWithLocalVariablesOfTypeIntAndBool(string assemblyPath)
        {
            Console.WriteLine("Get the name of a method with local variables of type bool and int");
            Assembly assembly = Assembly.LoadFrom(assemblyPath);

            //Este load me daba error porque no tenia instalado entity framework y la assembly tenia dependencia con EF. Si instalo EF me funciona.
            foreach (Type type in assembly.GetTypes())
            {
                foreach (MethodInfo method in type.GetMethods())
                {
                    var localVariables = method.GetMethodBody()?.LocalVariables;
                    
                    if (localVariables != null && localVariables.Count > 0)
                    {
                        bool hasInt = localVariables.Any(variable => variable.LocalType == typeof(int));
                        bool hasBool = localVariables.Any(variable => variable.LocalType == typeof(bool));

                        if (hasInt && hasBool)
                        {
                            Console.WriteLine($"Method {method.Name} in type {type.FullName} contains local variables of type int and bool.");
                        }
                    }
                }
            }

            Console.ReadLine();
            throw new NotImplementedException("Not implemented D:");
        }

        public MemberInfo GetTypeThatImplementsIEnumerable(string assemblyPath)
        {
            Console.WriteLine("Get the name of a type that implements IEnumerable");
            Assembly assembly = Assembly.LoadFrom(assemblyPath);

            MemberInfo memberInfo = null;
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetInterface("IEnumerable") is not null)
                {
                    memberInfo = type;
                    Console.WriteLine($"Type {type.Name} implements IEnumerable.");
                    //Falta instanciarlo, tener una variable de este tipo
                }
            }
            return memberInfo;
        }

        public MemberInfo GetTypeThatHasNestedTypeInSpanish(string assemblyPath)
        {
            Console.WriteLine("Get the name of a class member which has a nested type in spanish");
            Assembly assembly = Assembly.LoadFrom(assemblyPath);

            MemberInfo memberInfo = null;

            foreach (Type type in assembly.GetTypes())
            {

                // Check if the type has nested types
                foreach (Type nestedType in type.GetNestedTypes())
                {
                    Console.WriteLine(type.Name);
                }
            }

            return memberInfo;
        }

        static bool IsSpanish(string text)
        {
            // Basic check to determine if a string contains Spanish characters
            foreach (char c in text)
            {
                if ((c >= 'á' && c <= 'ú') || (c >= 'Á' && c <= 'Ú') || c == 'ñ' || c == 'Ñ')
                {
                    return true;
                }
            }
            return false;
        }
    }
}
