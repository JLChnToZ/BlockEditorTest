using System;
using System.IO;
using System.Windows.Forms;
using Jurassic;
using Jurassic.Library;

namespace BlockEditorTest {
    static class FileIOFunctions {
        public static void register(ScriptEngine engine, IWin32Window parentForm = null) {
            engine.SetGlobalFunction("getFiles", new Func<string, ObjectInstance>((s) => getFiles(s, engine).toArrayInstance(engine)));
            engine.SetGlobalFunction("getDirectories", new Func<string, ObjectInstance>((s) => getDirectories(s, engine).toArrayInstance(engine)));
            engine.SetGlobalFunction("readFile", new Func<string, string>((s) => readFile(s, engine)));
            engine.SetGlobalFunction("writeFile", new Action<string, string>((s, c) => writeFile(s, c, engine)));
            engine.SetGlobalFunction("deleteFile", new Action<string>((s) => deleteFile(s, engine)));
            engine.SetGlobalFunction("openFileDialog", new Func<string, string, bool, ObjectInstance>((p, t, m) => callOpenFileDialog(p, t, m, parentForm, engine).toArrayInstance(engine)));
            engine.SetGlobalFunction("saveFileDialog", new Func<string, string, string>((p, t) => callSaveFileDialog(p, t, parentForm, engine)));
        }

        private static JavaScriptException convertException(this Exception exception, ScriptEngine engine) {
            return new JavaScriptException(engine, exception.GetType().Name, exception.Message);
        }

        private static ArrayInstance toArrayInstance(this object[] rawArray, ScriptEngine engine) {
            try {
                return engine.Array.New(rawArray);
            } catch (Exception ex) {
                throw ex.convertException(engine);
            }
        }

        private static string[] getFiles(string path, ScriptEngine engine) {
            try {
                return Directory.GetFiles(path);
            } catch (Exception ex) {
                throw ex.convertException(engine);
            }
        }

        private static string[] getDirectories(string path, ScriptEngine engine) {
            try {
                return Directory.GetDirectories(path);
            } catch (Exception ex) {
                throw ex.convertException(engine);
            }
        }

        private static string readFile(string path, ScriptEngine engine) {
            try {
                return File.ReadAllText(path);
            } catch (Exception ex) {
                throw ex.convertException(engine);
            }
        }

        private static void writeFile(string path, string content, ScriptEngine engine) {
            try {
                File.WriteAllText(path, content);
            } catch (Exception ex) {
                throw ex.convertException(engine);
            }
        }

        private static void deleteFile(string path, ScriptEngine engine) {
            try {
                File.Delete(path);
            } catch (Exception ex) {
                throw ex.convertException(engine);
            }
        }

        private static string[] callOpenFileDialog(string startpath, string fileTypes, bool multiSelect, IWin32Window parentForm, ScriptEngine engine) {
            try {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.InitialDirectory = startpath;
                dlg.Filter = fileTypes;
                dlg.Multiselect = multiSelect;
                DialogResult result = parentForm == null ? dlg.ShowDialog() : dlg.ShowDialog(parentForm);
                return result == DialogResult.OK ? dlg.FileNames : new string[0];
            } catch (Exception ex) {
                throw ex.convertException(engine);
            }
        }

        private static string callSaveFileDialog(string startFileName, string fileTypes, IWin32Window parentForm, ScriptEngine engine) {
            try {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = startFileName;
                dlg.Filter = fileTypes;
                DialogResult result = parentForm == null ? dlg.ShowDialog() : dlg.ShowDialog(parentForm);
                return result == DialogResult.OK ? dlg.FileName : string.Empty;
            } catch (Exception ex) {
                throw ex.convertException(engine);
            }
        }
    }
}
