using Autodesk.Revit.UI;
using System;

namespace HelloWorld.View
{
    public interface IUserMessageFunc
    {
        void UserError(string userError);
        void UserMessage(string userMessage);
        void UserWarning(string userWarning);
    }

    public class UserMessageFunc : IUserMessageFunc
    {
        public void UserMessage(string userMessage)
        {
            TaskDialog.Show("Message", userMessage);
        }

        public void UserWarning(string userWarning)
        {
            TaskDialog.Show("Warning", userWarning);
        }

        public void UserError(string userError)
        {
            TaskDialog.Show("Error", userError);
        }
    }
}
