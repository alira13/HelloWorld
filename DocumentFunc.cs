using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Application = Autodesk.Revit.ApplicationServices.Application;

public class DocumentFunc
{
    public void CreateNewProjectByTemplate(Application app, FileInfo templateFile, FileInfo projectFile)
    {
        if (!templateFile.Exists) TaskDialog.Show("CreateNewProjectByTemplate ", templateFile.FullName + " does not exist in directory");
        else if (!projectFile.Directory.Exists) TaskDialog.Show("CreateNewProjectByTemplate ", projectFile.DirectoryName + " does not exist");
        else if (projectFile.Exists) TaskDialog.Show("CreateNewProjectByTemplate", $"{projectFile.Name} can not be created because it already exist in directory {projectFile.DirectoryName}");
        else
        {
            Document doc = null;
            doc = app.NewProjectDocument(templateFile.FullName);
            doc.SaveAs(projectFile.FullName);
            doc.Close();
            if (projectFile.Exists) TaskDialog.Show("CreateNewProjectByTemplate", projectFile.Name + " is succesfully created");
        }
    }

    public void CreateRevitLink(Document doc, string linkPathName, bool getCoordDoc = false)
    {
        //TODO: If link exist, show massage for user and skip command
        //TODO: Learn try-catch-finaly and use in code
        //TODO: If link has the same name as project file, show message and skip command
        // Create new revit link storing absolute path to a file
        RevitLinkInstance instance;

            Transaction transaction = new Transaction(doc);
            transaction.Start("Link files");
            ModelPath linkpath = ModelPathUtils.ConvertUserVisiblePathToModelPath(linkPathName);
            RevitLinkOptions options = new RevitLinkOptions(false);
            LinkLoadResult result = RevitLinkType.Create(doc, linkpath, options);
            //MessageBox.Show("Link ID " + result.ElementId.IntegerValue);
            instance = RevitLinkInstance.Create(doc, result.ElementId);
            //TODO: Test get coord from AR file
            if (getCoordDoc) doc.AcquireCoordinates(instance.Id);
            else
                instance.MoveBasePointToHostBasePoint(false);
            transaction.Commit();
            doc.Save();
            doc.Close();
    }

    public void EnableWorksharing(Document doc, string gridWorksetName, string levelWorksetName)
    {
       if (!doc.IsWorkshared)
        {
            doc.EnableWorksharing(gridWorksetName, levelWorksetName);
            SaveAsOptions options = new SaveAsOptions();
            WorksharingSaveAsOptions wsOptions = new WorksharingSaveAsOptions();

            wsOptions.SaveAsCentral = true;
            options.SetWorksharingOptions(wsOptions);
            options.OverwriteExistingFile = true;
            doc.SaveAs(doc.PathName, options);
            doc.Close();
        }
       else TaskDialog.Show("EnableWorksharing", "Worksharing is already enable in this file");
    }
    
    public void CreateWorksets(Document doc, string[] WorksetNameArray)
    {
        Workset newWorkset = null;
        // Worksets can only be created in a document with worksharing enabled

        string worksetName = "New Workset";
        // Workset name must not be in use by another workset
        for(int i=0; i< WorksetNameArray.Length; i++) {
            if (WorksetTable.IsWorksetNameUnique(doc, WorksetNameArray[i])) 
            {
                using (Transaction worksetTransaction = new Transaction(doc, "Set preview view id"))
                {
                    worksetTransaction.Start(WorksetNameArray[i]);
                    newWorkset = Workset.Create(doc, WorksetNameArray[i]);
                    worksetTransaction.Commit();
                }
            }
            else
            {
                TaskDialog.Show("CreateWorksets", $"Workset name {worksetName} is not unique");
            }
        }
        doc.Save();
        doc.Close();
    }

    public Document OpenNewLocalFromModelPath(Application app, ModelPath centralPath, ModelPath localPath)
    {
        // Create the new local at the given path
        WorksharingUtils.CreateNewLocal(centralPath, localPath);

        // Select specific worksets to open
        // First get a list of worksets from the unopened document
        IList<WorksetPreview> worksets = WorksharingUtils.GetUserWorksetInfo(localPath);
        List<WorksetId> worksetsToOpen = new List<WorksetId>();

        foreach (WorksetPreview preview in worksets)
        {
            // Match worksets to open with criteria
            if (preview.Name.StartsWith("O"))
                worksetsToOpen.Add(preview.Id);
        }

        // Setup option to open the target worksets
        // First close all, then set specific ones to open
        WorksetConfiguration worksetConfig = new WorksetConfiguration(WorksetConfigurationOption.CloseAllWorksets);
        worksetConfig.Open(worksetsToOpen);

        // Open the new local
        OpenOptions options1 = new OpenOptions();
        options1.SetOpenWorksetsConfiguration(worksetConfig);
        Document openedDoc = app.OpenDocumentFile(localPath, options1);

        return openedDoc;
    }
}

