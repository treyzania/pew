using System.IO;
using System.Collections.Generic;

namespace Pew.Util {
	
	// Largely stolen from http://stackoverflow.com/questions/485659/.
	public class Properties {
		
		private Dictionary<string, string> list;
		private string filename;
		
		public Properties(string file) {
			Reload(file);
		}
		
		public string Get(string field, string defValue) {
			return this.Get(field) == null ? defValue : this.Get(field);
		}
		
		public string Get(string field) {
			return list.ContainsKey(field) ? list[field] : null;
		}
		
		public void Set(string field, object value) {
			
			if (!list.ContainsKey(field)) {
				list.Add(field, value.ToString());
			} else {
				list[field] = value.ToString();
			}
			
		}
		
		public void Save() {
			Save(this.filename);
		}
		
		public void Save(string filename) {
			
			this.filename = filename;
			
			if (!File.Exists(filename))
				File.Create(filename);
			
			StreamWriter file = new StreamWriter(filename);
			
			foreach (string prop in list.Keys) {
				
				if (!string.IsNullOrEmpty(list[prop])) file.WriteLine(prop + "=" + list[prop]);
				
			}
			
			file.Close();
			
		}
		
		public void Reload() {
			Reload(this.filename);
		}
		
		public void Reload(string filename) {
			
			this.filename = filename;
			this.list = new Dictionary<string, string>();
			
			if (File.Exists(this.filename)) {
				LoadFromFile();
			} else {
				File.Create(this.filename);
			}
			
		}
		
		private void LoadFromFile() {
			
			/*
			 * Having an array reference like this instead of directly referencing it in the loop
			 * ensures that we can't accientally open the file again before we finish closing it.
			 */
			string[] lines = File.ReadAllLines(this.filename);
			
			foreach (string line in lines) {
				
				if ((!string.IsNullOrEmpty(line)) &&
				    (!line.StartsWith(";")) &&
				    (!line.StartsWith("#")) &&
				    (!line.StartsWith("'")) &&
				    (line.Contains("="))) {
				    
					int index = line.IndexOf('=');
					string key = line.Substring(0, index).Trim();
					string value = line.Substring(index + 1).Trim();
					
					if ((value.StartsWith("\"") && value.EndsWith("\"")) ||
					    (value.StartsWith("'") && value.EndsWith("'"))) {
						
						value = value.Substring(1, value.Length - 2);
						
					}
					
					try {
						
						//ignore duplicates
						list.Add(key, value);
						
					} catch {
						// Nothing much.
					}
					
				}
				
			}
			
		}
		
	}
	
}
