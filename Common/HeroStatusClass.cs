using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStatusClass {

		private string name;
		private string gender;
		private int skin;
		private string costume;
		private int costumeLv;
		private int heroism;
		private int status;
		private int requiretoback;
		private int health;
		private int maxHealth;
		private float atk;
		private float def;
		private int exp;


		public string Name{
			get {return name;}
			set {name = value;}
		}

		public string Gender{
			get {return gender;}
			set {gender = value;}
		}

		public int Skin{
			get {return skin;}
			set {skin = value;}
		}

		public string Costume{
			get {return costume;}
			set {costume = value;}
		}

		public int CostumeLv{
			get {return costumeLv;}
			set {costumeLv = value;}
		}

		public int Heroism{
			get {return heroism;}
			set {heroism = value;}
		}

		public int Status{
			get {return status;}
			set {status = value;}
		}

		public int RequireToBack{
			get {return requiretoback;}
			set {requiretoback = value;}
		}

		public int Health{
			get {return health;}
			set {health = value;}
		}

		public int MaxHealth{
			get {return maxHealth;}
			set {maxHealth = value;}
		}

		public float Atk{
			get {return atk;}
			set {atk = value;}
		}

		public float Def{
			get {return def;}
			set {def = value;}
		}

		public int Exp{
			get {
					if(exp == null){exp = 0;}
					return exp;
				}
			set {exp = value;}
		}

		public int Lv{
			get {return (exp / 100) + 1;}
		}

		public string StatusMessage (){
			string a = "HeroName : " + name  + "\nGender : "+ gender +"\nskin : "+ skin.ToString() +"\ncostume : "+ costume +"\ncostumeLv : "+ costumeLv.ToString() +"\nheroism : "+ heroism.ToString() +"\nstatus : " + status.ToString();
			return a;
		}

		public HeroStatusClass Clone(){
			HeroStatusClass retHero = new HeroStatusClass();
			retHero.Name = Name;
			retHero.Costume = Costume;
			retHero.CostumeLv = CostumeLv;
			retHero.Heroism = Heroism;
			return retHero;
		}
}
