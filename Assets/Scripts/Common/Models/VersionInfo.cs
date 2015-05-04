using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VersionInfo {
	string _recentVer;

	public string recentVer {
		get {
			return _recentVer;
		}
		set {
			_recentVer = value;
		}
	}

	string _supportVer;

	public string supportVer {
		get {
			return _supportVer;
		}
		set {
			_supportVer = value;
		}
	}

	string _FILE_PATH;

	public string FILE_PATH {
		get {
			return _FILE_PATH;
		}
		set {
			_FILE_PATH = value;
		}
	}

	string _FILE_SVR;

	string _APPS_SVR;

	public string APPS_SVR {
		get {
			return _APPS_SVR;
		}
		set {
			_APPS_SVR = value;
		}
	}

//	List<ServiceInfo> _FILE_SVR;
//
//	public List<ServiceInfo> FILE_SVR {
//		get {
//			return _FILE_SVR;
//		}
//		set {
//			_FILE_SVR = value;
//		}
//	}
//
//	List<ServiceInfo> _APPS_SVR;
//
//	public List<ServiceInfo> APPS_SVR {
//		get {
//			return _APPS_SVR;
//		}
//		set {
//			_APPS_SVR = value;
//		}
//	}

	public string FILE_SVR {
		get {
			return _FILE_SVR;
		}
		set {
			_FILE_SVR = value;
		}
	}
}
