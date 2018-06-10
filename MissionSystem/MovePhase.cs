using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePhase : MissionPhase {

	public string Type = "Move";
	
string destination 目的地
string distance 距離(near, middle, far)
int obstacle (0 ~ 100) 障害 遅くなる
string bridgemsg 着いた時に一言 default null
string mustmsg 絶対途中に入る言葉

}
