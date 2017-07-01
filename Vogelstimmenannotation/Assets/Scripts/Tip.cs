using System;
using UnityEngine;

public class Tip
{
    public Texture Image { get; private set; }
    public string Text { get; private set; }

	public Tip(Texture image, string text)
	{
        Image = image;
        Text = text;
	}
}
