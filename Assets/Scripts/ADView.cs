using UnityEngine;
using System.Collections;

public class ADView :MonoBehaviour
{
	public ADBannerView banner = null;

	void Start()
	{
		banner = new ADBannerView(ADBannerView.Type.Banner, ADBannerView.Layout.Bottom);
		ADBannerView.onBannerWasClicked += OnBannerClicked;
		ADBannerView.onBannerWasLoaded  += OnBannerLoaded;
		Object.DontDestroyOnLoad(transform.gameObject);
	}

	void OnBannerClicked()
	{

	}

	void OnBannerLoaded()
	{
		banner.visible = true;
	}
}