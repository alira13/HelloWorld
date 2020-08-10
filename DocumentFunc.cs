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
using Application = Autodesk.Revit.ApplicationServices.Application;

public class DocumentFunc
{

        public void CreateNewProjectByTemplate(Application app, FileInfo templateFile, FileInfo projectFile)
        {
            if (!templateFile.Exists) TaskDialog.Show("CreateNewProjectByTemplate ", templateFile.FullName + " does not exist in directory");
            else if (!projectFile.Directory.Exists) TaskDialog.Show("CreateNewProjectByTemplate ", projectFile.DirectoryName + " does not existy");
            else if (projectFile.Exists) TaskDialog.Show("CreateNewProjectByTemplate", $"{projectFile.Name} can not be created because it already exist in directory {projectFile.DirectoryName}");
            else
            {
                Document doc = app.NewProjectDocument(templateFile.FullName);
                doc.SaveAs(projectFile.FullName);
                if (projectFile.Exists) TaskDialog.Show("CreateNewProjectByTemplate", projectFile.Name + " is succesfully created");
            }
        }

        public void CreateRevitLink(Document doc, string linkPathName)
        {
            FilePath path = new FilePath(linkPathName);
            RevitLinkOptions options = new RevitLinkOptions(false);
            // Create new revit link storing absolute path to a file
            Transaction transaction = new Transaction(doc);
            transaction.Start("Link files");
            var linkType = RevitLinkType.Create(doc, path, options);
            var instance = RevitLinkInstance.Create(doc, linkType.ElementId);
            instance.MoveBasePointToHostBasePoint(false);
            transaction.Commit();
        }

        public Workset CreateWorksets(Document doc)
        {
            Workset newWorkset = null;
            // Worksets can only be created in a document with worksharing enabled
            if (!doc.IsWorkshared)
            {
                doc.EnableWorksharing("Оси", "Модель");

                SaveAsOptions options = new SaveAsOptions();
                WorksharingSaveAsOptions wsOptions = new WorksharingSaveAsOptions();

                wsOptions.SaveAsCentral = true;
                options.SetWorksharingOptions(wsOptions);
                options.OverwriteExistingFile = true;
                doc.SaveAs(doc.PathName, options);
            }

            string worksetName = "New Workset";
            // Workset name must not be in use by another workset
            if (WorksetTable.IsWorksetNameUnique(doc, worksetName))
            {
                using (Transaction worksetTransaction = new Transaction(doc, "Set preview view id"))
                {
                    worksetTransaction.Start();
                    newWorkset = Workset.Create(doc, worksetName);
                    worksetTransaction.Commit();
                }
            }
            else
            {
                TaskDialog.Show("CreateWorksets", $"Workset name {worksetName} is not unique");
            }

            return newWorkset;
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

