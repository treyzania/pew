using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.SocialPlatforms;
using System;
using System.Collections.Generic;
using Pew.Player;

// Largely from http://answers.unity3d.com/questions/894995/.
namespace Pew.Google {
	
	public class AndroidSaveSystem {
		
		public static Action OnSaveGameSelected;
		public static Action<SaveDataBundle> OnSaveLoaded;
		
		private static SaveDataBundle m_currentSaveBundle;
		private static ISavedGameMetadata m_saveBundleMetadata;
		
		/// <summary>
		/// Static reference to current save data. Automatically refreshed by save system.
		/// </summary>
		/// <value>The current save.</value>
		public SaveDataBundle CurrentSave {
			get {
				return m_currentSaveBundle;
			}
		}
		
		/// <summary>
		/// Shows the default save system UI. This allows user to select/delete saves as required.
		/// </summary>
		/// <param name="user">User.</param>
		/// <param name="callback">Invokes, when save game has been selected. Check for status for errors, or </param>
		public void ShowSaveSystemUI(ILocalUser user, Action<SelectUIStatus, ISavedGameMetadata> callback) {
			
			uint maxNumToDisplay = 3;
			bool allowCreateNew = true;
			bool allowDelete = true;
			
			ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
			
			if(savedGameClient != null) {
				
				savedGameClient.ShowSelectSavedGameUI(
					user.userName + "\u0027s saves",
					maxNumToDisplay,
					allowCreateNew,
					allowDelete,
					(SelectUIStatus status, ISavedGameMetadata saveGame) => {
						
						// some error occured, just show window again
						if (status != SelectUIStatus.SavedGameSelected) {
							ShowSaveSystemUI(user,callback);
							return;
						}
						
						if (callback != null) callback.Invoke(status, saveGame);
						
						if (OnSaveGameSelected != null && status == SelectUIStatus.SavedGameSelected) OnSaveGameSelected.Invoke();
					}
				);
				
			} else {
				
				// this is usually due to incorrect APP ID
				Debug.LogError("Save Game client is null...");
				
			}
			
		}
		
		/// <summary>
		/// Creates the new save. Save returned in callback is closed!. Open it before use.
		/// </summary>
		/// <param name="save">Save.</param>
		/// <param name="saveCreatedCallback">Invoked when save has been created.</param>
		private static void CreateNewSave(ISavedGameMetadata save, Action<ISavedGameMetadata> saveCreatedCallback) {
		
			ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
			
			SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder ();
			builder = builder
				.WithUpdatedPlayedTime(save.TotalTimePlayed.Add(new TimeSpan(0, 0, (int) Time.realtimeSinceStartup)))
				.WithUpdatedDescription("Saved at " + DateTime.Now);
			
			SavedGameMetadataUpdate updatedMetadata = builder.Build();
			
			SaveDataBundle newBundle = new SaveDataBundle(new StoredPlayerData());
			
			savedGameClient.CommitUpdate(
				save, 
				updatedMetadata, 
				SaveDataBundle.ToByteArray(newBundle), 
				(SavedGameRequestStatus status,ISavedGameMetadata game) => {
				
					if (status == SavedGameRequestStatus.Success) {
						m_saveBundleMetadata = game;
						if (saveCreatedCallback != null) saveCreatedCallback(game);
					}
					
					Debug.Log("Creating new save finished with status :" + status.ToString());
				
				}
			);
		}
		
		/// <summary>
		/// Opens the saved game.
		/// </summary>
		/// <param name="savedGame">Saved game.</param>
		/// <param name="callback">Invoked when game has been opened</param>
		private static void OpenSavedGame(ISavedGameMetadata savedGame, Action<ISavedGameMetadata> callback) {
			
			if (savedGame == null) return;
			
			if (!savedGame.IsOpen) {
				
				ISavedGameClient saveGameClient = PlayGamesPlatform.Instance.SavedGame;
				
				// save name is generated only when save has not been commited yet
				saveGameClient.OpenWithAutomaticConflictResolution(
					
					savedGame.Filename == string.Empty ? "Save" + UnityEngine.Random.Range(1000000,9999999).ToString() : savedGame.Filename,
					DataSource.ReadCacheOrNetwork,
					ConflictResolutionStrategy.UseLongestPlaytime,
					(SavedGameRequestStatus reqStatus, ISavedGameMetadata openedGame) => {
						
						if(reqStatus == SavedGameRequestStatus.Success) {
							m_saveBundleMetadata = openedGame;
							if(callback != null) callback.Invoke(m_saveBundleMetadata);
						}
						
					}
					
				);
				
			} else {
				if (callback != null) callback.Invoke(savedGame);
			}
			
		}
		
		
		/// <summary>
		/// Loads the saved game. This should be a starting point for loading data.
		/// </summary>
		/// <param name="user">User.</param>
		/// <param name="onSaveLoadedCallback">On save loaded callback.</param>
		public void LoadSavedGame(ILocalUser user, Action<SaveDataBundle> onSaveLoadedCallback) {
			
			if(onSaveLoadedCallback != null) OnSaveLoaded += onSaveLoadedCallback;
			
			if(m_saveBundleMetadata == null) {
				ShowSaveSystemUI(user, LoadGame);
			} else {
				LoadGame(SelectUIStatus.SavedGameSelected, m_saveBundleMetadata);
			}
			
		}
		
		static void LoadGame(SelectUIStatus status, ISavedGameMetadata game) {
			
			if (status == SelectUIStatus.SavedGameSelected) {
				
				OpenSavedGame(game, (ISavedGameMetadata openedGame) =>  {
					
					if(game.Description == null || game.Description == string.Empty) {
						
						// game has not been saved on cloud before, create new file
						CreateNewSave(openedGame, (ISavedGameMetadata newlySavedGame) => {
							LoadGame(SelectUIStatus.SavedGameSelected, newlySavedGame);
						});
						
						return;
						
					}
					
					// save should be opened now
					Debug.Log ("Loading save from: " + openedGame.Filename + "\n" + openedGame.Description + "\nOpened = " + openedGame.IsOpen.ToString ());
					m_saveBundleMetadata = openedGame;
					ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
					savedGameClient.ReadBinaryData(openedGame, (SavedGameRequestStatus reqStatus, byte[] data) =>  {
						
						Debug.Log("Reading save finished with status: " + reqStatus.ToString());
						
						if (reqStatus == SavedGameRequestStatus.Success) {
							
							SaveDataBundle bundle = SaveDataBundle.FromByteArray(data);
							m_currentSaveBundle = bundle;
							
							if (OnSaveLoaded != null) {
								OnSaveLoaded.Invoke (bundle);
								OnSaveLoaded = null;
							}
							
						}
						
					});
				});
			}
		}
		
		public void SaveGame(SaveDataBundle file, Action<bool> callback) {
			CommitSaveToCloud(file, "undefined", callback);
		}
		
		
		/// <summary>
		/// Commits the save to cloud.
		/// </summary>
		/// <param name="file">Actual save file. This will replace static reference to current save file</param>
		/// <param name="fileName">File name. Used only when saving for first time</param>
		/// <param name="callback">Invoked after commit (true = success)</param>
		private static void CommitSaveToCloud(SaveDataBundle file, string fileName, System.Action<bool> callback) {
			
			ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
			
			savedGameClient.OpenWithAutomaticConflictResolution(
				
				m_saveBundleMetadata.Filename == string.Empty ? fileName : m_saveBundleMetadata.Filename,
				DataSource.ReadCacheOrNetwork,
				ConflictResolutionStrategy.UseLongestPlaytime,
				(SavedGameRequestStatus reqStatus, ISavedGameMetadata openedGame) => {
					
					if(reqStatus == SavedGameRequestStatus.Success) {
						
						// adding real time since startup so we can determine longes playtime and resolve future conflicts easilly
						m_saveBundleMetadata = openedGame; 
						SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder ();
						builder = builder
							.WithUpdatedPlayedTime (m_saveBundleMetadata.TotalTimePlayed.Add (new TimeSpan (0, 0, (int)Time.realtimeSinceStartup)))
							.WithUpdatedDescription ("Saved game at " + DateTime.Now);
						
						SavedGameMetadataUpdate updatedMetadata = builder.Build ();
						
						savedGameClient.CommitUpdate(
							m_saveBundleMetadata,
							updatedMetadata,
							SaveDataBundle.ToByteArray(file),
							(SavedGameRequestStatus status,ISavedGameMetadata game) => {
								
								if (status == SavedGameRequestStatus.Success) {
									m_saveBundleMetadata = game;
									m_currentSaveBundle = file;
								}
							
							if (callback != null) callback.Invoke(status == SavedGameRequestStatus.Success);
							
						});
					
					}
				
				}
				
			);
			
		}
		
	}
	
	[System.Serializable]
	public class SaveDataBundle {
		
		public StoredPlayerData data;
		
		public SaveDataBundle(StoredPlayerData spd) {
			this.data = spd;
		}
		
		public static SaveDataBundle FromByteArray(Byte[] array) {
			
			if (array == null || array.Length == 0) {
				Debug.LogWarning("Serialization of zero sized array!");
				return null; 
			}
			
			using (var stream = new System.IO.MemoryStream(array)) {
				
				try {
					
					var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
					SaveDataBundle bundle = (SaveDataBundle)formatter.Deserialize(stream);
					return bundle;
					
				} catch (Exception e) {
					Debug.LogError("Error when reading stream: "+ e.Message);
				}
				
			}
			
			return null;
		}
		
		public static byte[] ToByteArray(SaveDataBundle bundle) {
			
			var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			
			using (var stream = new System.IO.MemoryStream()) {
				formatter.Serialize(stream, bundle);
				return stream.ToArray();
			}
			
		}
	}
	
	public class GoogleFrontend {
		
		private static bool Initialized;
		private static AndroidSaveSystem SaveSystem;
		
		public static bool UseLocalStorage;
		
		private GoogleFrontend() {} // Let's not initialize this, m'kay?
		
		public static void Init(Action<bool> callback) {
			
			OnScreenLog.Log("Initializing social integration...");
			
			if (!Initialized) {
				
				PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
					.EnableSavedGames()
					.Build();
				
				PlayGamesPlatform.InitializeInstance(config);
				PlayGamesPlatform.DebugLogEnabled = true;
				PlayGamesPlatform.Activate();
				
				Social.localUser.Authenticate((bool success) => {
					
					OnScreenLog.Log("Auth success: " + success);
					Debug.Log("Sign in status: " + success);
					
					UseLocalStorage = !success; // :/
					
					//SaveSystem = new AndroidSaveSystem();
					
					if (success) {
						
						SaveSystem = new AndroidSaveSystem();
						
					} else {
						
						// Okaaaaay...  This is going to be painful.
						
					}
					
					// Must be done after everything else is initialized.
					callback.Invoke(success);
					
				});
				
				Initialized = true;
				
			}
			
		}
		
		public static void LoadGame(Action postLoad) {
			
			OnScreenLog.Log("Loading game...");
			StoredPlayerData local = StoredPlayerData.LocalLoad();
			StoredPlayerData cloud = null;
			
			if (SaveSystem != null) {
				
				OnScreenLog.Log("Save system not null. (GOOD)");
				
				SaveSystem.LoadSavedGame(Social.localUser, (SaveDataBundle dataBundle) => {
					
					OnScreenLog.Log("SaveSys LSG callback invoked.");
					
					cloud = dataBundle.data;
					StoredPlayerData.WasLocalSave = false;
					
					Debug.Log("Locally loaded game: " + local);
					
					// Load the save times data.  Should this data be obfuscated or encrypted?
					DateTime localSaveTime = StoredPlayerData.GetTimeValue(StoredPlayerData.TIME_LOCAL_KEY);
					DateTime cloudSaveTime = StoredPlayerData.GetTimeValue(StoredPlayerData.TIME_CLOUD_KEY);
					
					Debug.Log("LOCAL time: " + localSaveTime + "; CLOUD time: " + cloudSaveTime);
					OnScreenLog.Log("LOCAL time: " + localSaveTime + "; CLOUD time: " + cloudSaveTime);
					Debug.Log("Using most recent save...");
					StoredPlayerData.PLAYER_DATA = (localSaveTime >= cloudSaveTime) ? local : cloud;
					
					OnScreenLog.Log("Invoking postload callback...");
					postLoad.Invoke();
					
				});
				
			} else {
				Debug.LogWarning("Save system is null!");
				OnScreenLog.Log("Save system is null!");
			}
			
		}
		
		public static void Save() {
			
			StoredPlayerData data = StoredPlayerData.PLAYER_DATA;
			
			// Try cloud storage.
			if (SaveSystem != null && SaveSystem.CurrentSave != null) {
				
				Debug.Log("SaveSystem and its current save are non-null.");
				
				SaveSystem.SaveGame(new SaveDataBundle(data), (bool success) => {
					
					if (success) {
						
						StoredPlayerData.UpdateTimeValue(StoredPlayerData.TIME_CLOUD_KEY);
						
					} else {
						
						Debug.LogWarning("Cloud save failed!");
						
					}
					
				});
				
			} else {
				Debug.LogWarning("SaveSystem and/or its current save may be null!");
			}
			
			// Always do local storage.
			data.LocalSave();
			
		}
		
	}
	
}
