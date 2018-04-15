using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicantClass : HeroStatusClass {

	private string message;
	private string personality1;
	private string personality2;
	private int motivation;

	public string Message{
		get {return message;}
		set {message = value;}
	}

	public string Personality1{
		get {return personality1;}
		set {personality1 = value;}
	}

	public string Personality2{
		get {return personality2;}
		set {personality2 = value;}
	}

	public int Motivation{
		get {return motivation;}
		set {motivation = value;}
	}

}
