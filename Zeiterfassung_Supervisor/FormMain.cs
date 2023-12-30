/* Zeit - Arbeitszeiterfassung (Client)
 * 
 * 
 * 
 * Entwickelt von: Christoph Beyer
 * Build Datum: 08/2023
 * 
 * Copyright (c) 2022 - 2023 Christoph Beyer
 */

using ListViewSorter;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Zeiterfassung_Supervisor
{
    public partial class FormMain : Form
    {
        #region Variables
        string username;
        string localFolder = System.IO.Path.GetTempPath() + @"\Zeiterfassung";
        string logFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ZeifSupvisr_ErrorLog.txt";
        int fontSize = 10;   //font size at start
        string signedInUser;                                    //loaded from Form1
        int rightsLevel;                                        //loaded from Form1 (0 = names and sync; 1 = names, sync and hours; 2 = names, sync, and excel; 3 = names, sync, hours and excel)
        string allowedUsers;                                    //loaded from Form1
        string sourceFolder = "";                               //loaded from conf file
        string viewFileSourceFolder = "";                       //loaded from conf file
        string programPath = "";                                //loaded from conf file
        string supportEmail = "christoph.beyer@schindler.com";  //loaded from conf file
        int highlightOldSyncDays;                               //loaded from conf file

        private ListViewColumnSorter lvwColumnSorter;
        #endregion

        public FormMain(string signedInUser, int rightsLevel, string allowedUsers)
        {
            InitializeComponent();

            //Setter Form1
            this.signedInUser = signedInUser;
            this.rightsLevel = rightsLevel;
            this.allowedUsers = allowedUsers;

            this.Text = this.Text += " (" + signedInUser + ", Level " + rightsLevel + ")";
        }


        #region Tools

        private void LoadConfig()
        {
            try
            {
                sourceFolder = Properties.Settings.Default.SourceFolder;
                viewFileSourceFolder = Properties.Settings.Default.ViewFilePapth;
                programPath = Properties.Settings.Default.ProgramPath;
                if (Properties.Settings.Default.ProgramPathUser != string.Empty) programPath = Properties.Settings.Default.ProgramPathUser;
                supportEmail = Properties.Settings.Default.SupportEmail;
                highlightOldSyncDays = Properties.Settings.Default.HightlightSyncdateIfOlder;
            }

            catch (Exception ex)
            {
                ERROR("LoadConfig", ex.Message, "");
            }
        }

        public string GetDateTime()
        {
            return DateTime.Now.ToString();
        }

        public void ERROR(string functionName, string errMsg)
        {
            try
            {
                string dt = GetDateTime();
                string addStream = dt + ": [" + functionName + "] \"" + errMsg + "\"";

                if (File.Exists(logFilePath))
                {
                    string fileStream = File.ReadAllText(logFilePath);
                    File.WriteAllText(logFilePath, fileStream + "\n" + addStream);
                }

                else
                {
                    File.WriteAllText(logFilePath, addStream);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim erstellen der Logdatei. Wenden Sie sich an ihren lokalen IT-Support:\n\n" + supportEmail + "\n\nError: " + ex.Message, "Fataler Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ERROR(string functionName, string errMsg, string dispMessage)
        {
            try
            {
                string dt = GetDateTime();
                string addStream = dt + ": [" + functionName + "] \"" + errMsg + "\"";

                if (File.Exists(logFilePath))
                {
                    string fileStream = File.ReadAllText(logFilePath);
                    File.WriteAllText(logFilePath, fileStream + "\n" + addStream);
                }

                else
                {
                    File.WriteAllText(logFilePath, addStream);
                }

                MessageBox.Show("Es ist ein Fehler aufgetreten.\n\n" + dispMessage, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim erstellen der Logdatei. Wenden Sie sich an ihren lokalen IT-Support:\n\n" + supportEmail + "\n\nError: " + ex.Message, "Fataler Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckProgramExists()
        {
            if (File.Exists(programPath)) return true;
            else
            {
                MessageBox.Show("Der Programpfad \"" + programPath + "\" wurde nicht gefunden.\n\nGeben Sie in der Konfigurationsdatei den korrekten Programm Dateipfad an." +
                    "\n\n\nSie können die Anwendung weiterhin nutzen, jedoch keine Excel Tabellen öffnen.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void ClearCreateFolder()
        {
            try
            {
                if (File.Exists(localFolder)) Directory.Delete(localFolder, true);
                Directory.CreateDirectory(localFolder);
            }

            catch (Exception ex)
            {
                ERROR("ClearFolder", ex.Message, "");
            }
        }

        private void LoadList()
        {
            listView.Items.Clear();

            try
            {
                DirectoryInfo d = new DirectoryInfo(viewFileSourceFolder);

                string errNames = "";

                foreach (var file in d.GetFiles("*.szt"))
                {
                    try
                    {
                        username = file.Name.Substring(0, file.Name.IndexOf('.')).ToUpper();

                        if (allowedUsers.ToUpper().Contains(username.ToUpper()) || allowedUsers.ToUpper() == "ALL")
                        {
                            string fileStream = File.ReadAllText(file.FullName);
                            string fileText = Decrypt(fileStream, CreateKey(), CreateIV());

                            string[] lines = new string[6];

                            using (StringReader reader = new StringReader(fileText))
                            {
                                int i = 0;
                                string line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    if (rightsLevel == 1 || rightsLevel == 3)
                                    {
                                        lines[i] = line;
                                        i++;
                                    }

                                    else
                                    {
                                        if (i > 2 && i < 4)
                                        {
                                            lines[i] = "(keine Berechtigung zum Anzeigen dieses Wertes)";
                                        }
                                        else lines[i] = line;
                                        i++;
                                    }
                                }
                            }

                            ListViewItem lvi = new ListViewItem(lines);
                            if (rightsLevel == 2 || rightsLevel == 3) lvi.ToolTipText = "(Doppelklicken zum öffnen der Excel Tabelle)";

                            listView.Items.Add(lvi);
                        }
                    }

                    catch (Exception ex)
                    {
                        if (allowedUsers.ToUpper().Contains(username.ToUpper()) || allowedUsers.ToUpper() == "ALL") errNames += username + "\n";
                        //ERROR("LoadList", ex.Message + " File: " + username);
                    }
                }

                if (errNames != string.Empty) MessageBox.Show("Von den nachfolgenden Mitarbeitern konnte die Liste nicht geladen werden. Möglicherweise sind die Daten beschädigt oder unvollständig:\n\n" + errNames, "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Warning);    
            }

            catch (Exception ex)
            {
                ERROR("LoadList", ex.Message, "Die Liste konnte nicht geladen werden.");
            }

            HighlightSuspValuesInList();
        }

        private void DecryptAndOpenExcel()
        {
            try
            {
                if (rightsLevel == 2 || rightsLevel == 3)
                {
                    if (CheckProgramExists())
                    {
                        username = listView.SelectedItems[0].Text.ToUpper();
                        if (username.Contains("*")) username = username.Replace("*", "");
                        string excelFilepath = sourceFolder + @"\" + listView.SelectedItems[0].Text.Replace("*", "") + ".zt";

                        if (File.Exists(excelFilepath))
                        {
                            //Decrypt and open Excel file
                            string filepath = localFolder + @"\" + username.ToUpper() + ".xlsx";
                            Byte[] bytes = File.ReadAllBytes(excelFilepath);
                            String file = Convert.ToBase64String(bytes);

                            Byte[] bytes2 = Convert.FromBase64String(Decrypt(file, CreateKey(), CreateIV()));
                            File.WriteAllBytes(filepath, bytes2);
                            Process.Start(programPath, "\"" + filepath + "\"");
                        }

                        else
                        {
                            MessageBox.Show("Die Excel Tabelle konnte nicht geöffnet werden, da die Datei nicht gefunden wurde.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                else MessageBox.Show("Sie sind zum öffnen der Excel Tabellen nicht berechtigt.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception ex)
            {
                ERROR("DecryptAndOpenExcel", ex.Message, "Die Excel Tabelle konnte nicht geöffnet werden.");
            }
        }

        private void HighlightSuspValuesInList()
        {
            try
            {
                //hightlight suspicious values in list

                for (int i = 0; i < listView.Items.Count; i++) //old sync days
                {
                    DateTime syncDate = DateTime.Parse(listView.Items[i].SubItems[2].Text);

                    double elapsedTime = ((syncDate - DateTime.Now).TotalDays) * (-1);

                    if (elapsedTime > highlightOldSyncDays)
                    {
                        listView.Items[i].UseItemStyleForSubItems = false;
                        listView.Items[i].SubItems[2].ForeColor = Color.Red;
                    }
                }

                for (int i = 0; i < listView.Items.Count; i++) //wrong year
                {
                    if (listView.Items[i].SubItems[5].Text != DateTime.Now.Year.ToString())
                    {
                        listView.Items[i].UseItemStyleForSubItems = false;
                        listView.Items[i].SubItems[5].ForeColor = Color.Red;
                    }
                }


                for (int i = 0; i < listView.Items.Count; i++) //wrong year
                {
                    for (int j = 0; j < 6; j++)
                    {
                        listView.Items[i].SubItems[j].Font = new Font("Segoe UI", fontSize, FontStyle.Regular);
                    }
                }
            }

            catch (Exception ex)
            {
                ERROR("HighlightSuspValuesInList", ex.Message, "Fehler beim markieren der Werte.");
            }
        }

        #endregion


        #region Decryption
        private string Decrypt(string encrypted, string key, string iv)
        {
            try
            {
                byte[] encbytes = Convert.FromBase64String(encrypted);
                AesCryptoServiceProvider encdec = new AesCryptoServiceProvider();
                encdec.BlockSize = 128;
                encdec.KeySize = 128;
                encdec.Key = ASCIIEncoding.ASCII.GetBytes(key);
                encdec.IV = ASCIIEncoding.ASCII.GetBytes(iv);
                encdec.Padding = PaddingMode.PKCS7;
                encdec.Mode = CipherMode.CBC;

                ICryptoTransform icrypt = encdec.CreateDecryptor(encdec.Key, encdec.IV);

                byte[] dec = icrypt.TransformFinalBlock(encbytes, 0, encbytes.Length);
                icrypt.Dispose();

                return ASCIIEncoding.ASCII.GetString(dec);
            }

            catch
            {
                return null;
            }
        }

        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        private String CreateKey()
        {
            string key = GetHashString(username.ToUpper() + username.Length).Substring(0, 32);
            return key;
        }

        private String CreateIV()
        {
            return GetHashString(username.ToUpper()).Substring(0, 16);
        }

        #endregion


        #region GUI

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DecryptAndOpenExcel();
        }


        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadConfig();
            ClearCreateFolder();
            LoadList();
            //CheckProgramExists();

            //copyright year
            if (DateTime.Now.Year > 2023)
            {
                lblCopyright.Text = "Copyright © 2022 - " + DateTime.Now.Year + " Christoph Beyer, Schindler AG";
            }

            lvwColumnSorter = new ListViewColumnSorter();
            this.listView.ListViewItemSorter = lvwColumnSorter;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnSignout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (fontSize > 2 && fontSize < 50)
                {
                    listView.Font = new Font("Segoe UI Semibold", ++fontSize);
                    HighlightSuspValuesInList();
                }
            }

            catch (Exception ex)
            {
                ERROR("IncreaseFontSize*", ex.Message);
            }
        }

        private void btnSizeMinus_Click(object sender, EventArgs e)
        {
            try
            {
                if (fontSize > 3 && fontSize < 51)
                {
                    listView.Font = new Font("Segoe UI Semibold", --fontSize, listView.Font.Style);
                    HighlightSuspValuesInList();
                }
            }

            catch (Exception ex)
            {
                ERROR("DecreaseFontSize*", ex.Message);
            }
        }

        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                ListView myListView = (ListView)sender;

                // Determine if clicked column is already the column that is being sorted.
                if (e.Column == lvwColumnSorter.SortColumn)
                {
                    // Reverse the current sort direction for this column.
                    if (lvwColumnSorter.Order == SortOrder.Ascending)
                    {
                        lvwColumnSorter.Order = SortOrder.Descending;
                    }
                    else
                    {
                        lvwColumnSorter.Order = SortOrder.Ascending;
                    }
                }
                else
                {
                    // Set the column number that is to be sorted; default to ascending.
                    lvwColumnSorter.SortColumn = e.Column;
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }

                // Perform the sort with these new sort options.
                myListView.Sort();
            }

            catch (Exception ex) 
            {
                ERROR("listViewSort", ex.Message);
            }
        }

        #endregion
    }
}
