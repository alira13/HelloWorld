using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace HelloElemCom
{
    class HelloElemCom : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication rvtUIApp;
            UIDocument rvtUIDoc;
            Autodesk.Revit.ApplicationServices.Application m_rvtApp;
            Document m_rvtDoc;

            DocumentFunc docFuc = new DocumentFunc();

            rvtUIApp = commandData.Application;
            //rvtUIDoc = rvtUIApp.ActiveUIDocument;
            m_rvtApp = rvtUIApp.Application;
            //m_rvtDoc = rvtUIDoc.Document;

            /*
            Element[] elementArray = GetAllElemOfCategory(m_rvtDoc, BuiltInCategory.OST_Walls);
            //PrintElemInfo(elementArray);

            Element el = PickElement(rvtUIDoc);
            ShowBasicElementInfo(m_rvtDoc, el);
            IdentifyElem(el);
            ShowParameters(el, "ShowParameters");
            RetrieveParameter(el, "RetrieveParameter");
            ShowLocation(el);
            ShowGeometry(m_rvtApp, el);
            */
            return Result.Succeeded;
        }
    }
}
