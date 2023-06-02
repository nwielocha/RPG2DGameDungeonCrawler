using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject 
{
	public string objectName;
	public Sprite sprite; // referencja do duszka, aby mozna go wyswietlic w grze
	public int quantity;
	public bool stackable; // identyczne kopie przedmiotow moga byc przechowywane w tym samym slocie
	public ItemType itemType;
}
