using Microsoft.VisualBasic.FileIO;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Zeiterfassung_Supervisor
{
    public partial class Form1 : Form
    {
        public static MemoryStream GenerateStreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }

        #region Varbiables
        string username = Environment.UserName;
        string dbStream;
        string logFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ZeifSupvisr_ErrorLog.txt";
        string databasePath = "";                               //loaded from config file
        string supportEmail = "christoph.beyer@schindler.com";  //loaded from config file

        int rightsLevel;
        string allowedUsers;

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        #region Tools
        private void ReportError()
        {
            try
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo("mailto:" + supportEmail) { UseShellExecute = true });
            }

            catch
            {
                MessageBox.Show("Senden Sie eine Mail mit ihrer Fehlerbeschreibung an folgende Adresse:" +
                    "\n\n" + supportEmail, "Fehler melden", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            catch
            {
                MessageBox.Show("Es konnte keine Logdatei erstellt werden. Wenden Sie sich an ihren lokalen IT-Support:\n\n" + supportEmail, "Fataler Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Forms.Application.Exit();
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

            catch
            {
                MessageBox.Show("Fataler Fehler, das Programm wird beendet. Wenden Sie sich an ihren lokalen IT-Support:\n\n" + supportEmail, "Fataler Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Forms.Application.Exit();
            }
        }

        private void LoadConfig()
        {
            try
            {
                databasePath = Properties.Settings.Default.DatabasePath;
                supportEmail = Properties.Settings.Default.SupportEmail;
            }

            catch (Exception ex)
            {
                ERROR("LoadConfig", ex.Message, "");
            }
        }

        private bool DecryptLoadDatabase()
        {
            try
            {
                dbStream = Decrypt(File.ReadAllText(databasePath), CreateKey(), CreateIV());
                return true;
            }

            catch (Exception ex)
            {
                ERROR("LoadDatabase", ex.Message);
                return false;
            }
        }

        private bool CheckUserIsAuthorized()
        {
            try
            {
                string allUsernames = "";
                using (StringReader reader = new StringReader(dbStream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        allUsernames += line.Substring(0, line.IndexOf(";"));
                    }
                }

                if (allUsernames.ToUpper().Contains(username.ToUpper())) return true;
            }

            catch (Exception ex)
            {
                ERROR("CheckUserIsAuthorized", ex.Message);
            }

            return false;
        }

        private void LoadUserinfoFromDB()
        {
            try
            {
                Stream str = GenerateStreamFromString(Decrypt(File.ReadAllText(databasePath), CreateKey(), CreateIV()));
                using (TextFieldParser csvParser = new TextFieldParser(str))
                {
                    csvParser.CommentTokens = new string[] { "#" };
                    csvParser.SetDelimiters(new string[] { ";" });
                    //csvParser.HasFieldsEnclosedInQuotes = false;

                    // Skip the row with the column names
                    //csvParser.ReadLine();

                    bool cont = true;

                    while (cont && !csvParser.EndOfData)
                    {
                        string[] fields = csvParser.ReadFields();

                        if (fields[0].Equals(username, StringComparison.InvariantCultureIgnoreCase))
                        {
                            rightsLevel = Convert.ToInt32(fields[1]);
                            allowedUsers = fields[2];
                            cont = false;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                ERROR("LoadUsernamesFromDB", ex.Message, "");
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
            string key = GetHashString("christophbeyer" + 2022).Substring(0, 32);
            return key;
        }

        private String CreateIV()
        {
            return GetHashString("beyerch").Substring(0, 16);
        }

        #endregion

        #region GUI

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadConfig();

            btnSignIn.Text = "Als " + username.ToUpper() + " &anmelden";


            //copyright year
            if (DateTime.Now.Year > 2023)
            {
                lblCopyright.Text = "Copyright © 2022 - " + DateTime.Now.Year + " Christoph Beyer, Schindler AG";
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool loadedDatabase = DecryptLoadDatabase();

            if (loadedDatabase && CheckUserIsAuthorized())
            {
                LoadUserinfoFromDB();
                FormMain frmMain = new FormMain(username, rightsLevel, allowedUsers);
                frmMain.ShowDialog();
            }

            else if (loadedDatabase)
            {
                MessageBox.Show("Sie besitzen keine Berechtigung für das Anzeigen der Gleitzeitinformationen.\n\nSollten Sie berechtigt sein, wenden Sie sich bitte an die IT: " + supportEmail, "Keine Berechtigung", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                MessageBox.Show("Die Datenbank konnte nicht geladen werden. Stellen Sie sicher, dass eine Internetverbindung besteht und versuchen Sie es erneut.\n\nSollte das Problem weiterhin bestehen, wenden Sie sich bitte an die IT: " + supportEmail, "Keine Berechtigung", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            FormAbout frmAbt = new FormAbout();
            frmAbt.ShowDialog();
        }

        private void btnReportError_Click(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
               try
               {
                   if (File.Exists(logFilePath)) Process.Start("notepad.exe", logFilePath);

                   else MessageBox.Show("Keine Log-Datei vorhanden");
               }

               catch (Exception ex)
               {
                    ERROR("OpenLogFile(*)", ex.Message, "");
                }
            }

            else if (ModifierKeys.HasFlag(Keys.Alt) && ModifierKeys.HasFlag(Keys.Shift))
            {
                try
                {
                    Properties.Settings.Default.HightlightSyncdateIfOlder = Properties.Settings.Default.HightlightSyncdateIfOlder;
                    Properties.Settings.Default.ProgramPathUser = Properties.Settings.Default.ProgramPathUser;
                    Properties.Settings.Default.EnableScaling = Properties.Settings.Default.EnableScaling;
                    Properties.Settings.Default.Save();
                    Process.Start("explorer.exe", System.IO.Path.GetDirectoryName(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath));
                }

                catch (Exception ex)
                {
                    ERROR("OpenAppDataFolder(*)", ex.Message, "");
                }
            }

            else ReportError();
        }

        #endregion
    }
}