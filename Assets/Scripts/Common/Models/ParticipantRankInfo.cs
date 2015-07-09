using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ParticipantRankInfo {
	int _rankSeq;

	public int rankSeq {
		get {
			return _rankSeq;
		}
		set {
			_rankSeq = value;
		}
	}
	int _memSeq;
	
	public int memSeq {
		get {
			return _memSeq;
		}
		set {
			_memSeq = value;
		}
	}
	int _rank;
	
	public int rank {
		get {
			return _rank;
		}
		set {
			_rank = value;
		}
	}
	int _rewardValue;
	
	public int rewardValue {
		get {
			return _rewardValue;
		}
		set {
			_rewardValue = value;
		}
	}
	int _rewardType;
	
	public int rewardType {
		get {
			return _rewardType;
		}
		set {
			_rewardType = value;
		}
	}

}
