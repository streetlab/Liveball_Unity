using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoneMailinfo {
	
	private string _outMessage;
	public string outMessage {
		get {
			return _outMessage;
		}
		set {
			_outMessage = value;
		}
	}
	

	private int _outCode;
	public int outCode {
		get {
			return _outCode;
		}
		set {
			_outCode = value;
		}
	}

}
