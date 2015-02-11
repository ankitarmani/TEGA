using System;

namespace TEGACore
{
	public class CoreConstants
	{
		// Server constants
//		public const string ServerUrl = "ws://192.168.1.161:8080";
		public const string ServerUrl = "ws://max2.humance.de:8084";
		public const string ServerContext = "/logiassist-ws-3.0.0-SNAPSHOT/ws";
		public const string ServerProtocol = "logiassist-protocol";
		public const string WebSocketDefaultDeviceName = "android";
		public const string WebSocketAddressSuffix = "@logiassist.de";
		public const string WebSocketServerAddress = "server@logiassist.de";
		public const string ServerHttpUrl = "http://192.168.1.7:8080";
		//		public const string ServerHttpUrl = "http://max2.humance.de:8084";
		public const string ServerRestContext = "/logiassist-ws-3.0.0-SNAPSHOT/rest";
		public const string ServerEducationRestContext = "/education";
		
		// WS message static data
		public const string WsDataOk = "OK";
		public const string WsDataNok = "NOK";
		
		// WS message type
		public const string WsMsgTypeConnectDevice = "de.humance.logiassist.ws.token.ConnectDeviceTokenAction";
		public const string WsMsgTypeIdentifyDevice = "de.humance.logiassist.ws.token.IdentifyDeviceTokenAction";
		public const string WsMsgTypeGetListTypes = "de.humance.logiassist.ws.token.GetListTypesTokenAction";
		public const string WsMsgTypeCheckUserNameAvail = "de.humance.logiassist.ws.token.CheckUserNameAvailTokenAction";
		public const string WsMsgTypeGetAvailOrgNames = "de.humance.logiassist.ws.token.GetAvailOrgNamesTokenAction";
		public const string WsMsgTypeRegister = "de.humance.logiassist.ws.token.RegisterTokenAction";
		public const string WsMsgTypeLogIn = "de.humance.logiassist.ws.token.LogInTokenAction";
		public const string WsMsgTypeGetUserInfo = "de.humance.logiassist.ws.token.GetUserInfoTokenAction";
		public const string WsMsgTypeGetOrgInfo = "de.humance.logiassist.ws.token.GetOrgInfoTokenAction";
		public const string WsMsgTypeGetUserEducationInfo = "de.humance.logiassist.ws.token.GetUserEducationInfoTokenAction";
		public const string WsMsgTypeGetLoContent = "de.humance.logiassist.ws.token.GetLearningObjectContentTokenAction";
		public const string WsMsgTypeUpdateUserPortrait = "de.humance.logiassist.ws.token.UpdateUserPortraitTokenAction";
		
		// ClassName constants
		public const string ClassNameGeoLocation = "de.humance.logiassist.model.GeoLocation";
		public const string ClassNamePoi = "de.humance.logiassist.model.Poi";
		public const string ClassNamePoiAssociation = "de.humance.logiassist.model.PoiAssociation";
		public const string ClassNamePoiType = "de.humance.logiassist.model.PoiType";
		public const string ClassNamePoiService = "de.humance.logiassist.model.PoiService";
		public const string ClassNamePoiAttributeDefinition = "de.humance.logiassist.model.PoiAttributeDefinition";
		public const string ClassNamePoiAttribute = "de.humance.logiassist.model.PoiAttribute";
		public const string ClassNameContact = "com.liferay.portal.model.Contact";
		
		// Poi Type names
		public const string PoiTypeNameDoctor = "Doctor";
		public const string PoiTypeNameDentist = "Dentist";
		public const string PoiTypeNameOphthalmologist = "Ophthalmologist";
		public const string PoiTypeNameHospital = "Hospital";
		public const string PoiTypeNameRestArea = "RestArea";
		
		// Shared Preferences
		public const string SharedPrefsName = "logiassist-shared-prefs";
		public const string PrefRememberPassword = "pref-remember-password";
		public const string PrefLogin = "pref-login";
		public const string PrefPassword = "pref-password";
		
		// Database name
		public const string DatabaseEmptyName = "TEGA.db";
		public const string DatabaseName = "TEGA.dat";
		public const string DatabasePwd = "a54SK4814N51ihT3L34k5n7727hGq6";
		
		// Encryption
		public const string EncryptionType = "AES";
		//		public const string EncryptionCipherType = "AES/CTS/PKCS5Padding";
		public const string EncryptionCipherType = "AES/CBC/PKCS5Padding";
		public const string EncryptionPwd = "nw7mM81At37mI7rH";
		
		// Folder name
		public const string FolderNameLogiAssist = "TEGA";
		public const string FolderNameEducation = "edu";
		public const string FolderNameOrganization = "org";
		public const string FolderNamePrivate = "pri";
		
		// File extension
		public const string FileExtData = "dat";
		public const string FileExtLoPrimary = "pri";
		public const string FileExtLoSecondary = "sec";
	}
}

