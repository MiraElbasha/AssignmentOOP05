using System.Security.AccessControl;

namespace AssignmentOOP05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Part1
            #region Q1
            //An interface in C# is a contract that defines a set of methods, properties..etc without implementing them. It specifies what a class must do
            //we use it to reduce dependency and to achieve abstraction and polymorphism in our code also Instead of depending on a specific class, we depend on a contract (interface).

            /*
              1- loose coupling : which makes the system easier to modify becuase classes depends on interfaces not on specific implementations
              2- Multiple Implementation : Different classes can implement the same interface in different ways.
              3- Better Maintainability : Changes to the implementation of an interface do not affect the classes that use it
            */
            #endregion

            #region Q2
            // both interfaces share the same implementation, which means the program cannot differentiate between English greeting and Arabic greeting.

            //We fix it using Explicit Interface Implementation so Each interface method is implemented separately.

            //No, you cannot call translator.Greet directly. Because when we use explicit interface implementation the methods are not accessible through the class itself.
            #endregion
        #endregion

        }
    }
}
