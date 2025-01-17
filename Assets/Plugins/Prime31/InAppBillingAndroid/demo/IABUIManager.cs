using System;
using UnityEngine;
using System.Collections.Generic;
using Prime31;



namespace Prime31
{
	public class IABUIManager : MonoBehaviourGUI
	{
#if UNITY_ANDROID
		List<GoogleSkuInfo> skus;

		void Awake()
		{
			GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		}

		void queryInventorySucceededEvent(List<GooglePurchase> purchases, List<GoogleSkuInfo> skus)
		{
			this.skus = skus;
		}


		void OnGUI()
		{
			beginColumn();

			if( GUILayout.Button( "Initialize IAB" ) )
			{
				var key = "your public key from the Android developer portal here";
				key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAmffbbQPr/zqRjP3vkxr1601/eKsXm5kO2NzQge8m7PeUj5V+saeounyL34U8WoZ3BvCRKbw6DrRLs2DMoVuCLq7QtJggBHT/bBSHGczEXGIPjWpw6OQb24EWM0PaTRTH2x2mC/X6RwIKcPLJFmy68T38Eh0DXnF4jjiIoaD0W8AYLjLzv0WvbIfgtJlvmmwvI2/Kta1LRnW3/Ggi5jb9UmXZAUIBz8kQtSH5FUCmFOQHMzekfg8rQ4VO1nlWhnB58UPwsxWt/DNyDfqv2VMeA2+VJG0fkiMl/6vWA7+ianVTU3owXcvxJHseEDUVYo1wEKfhK7ErGB7sxDJx5wHXAwIDAQAB";
				GoogleIAB.init( key );
			}


			if( GUILayout.Button( "Query Inventory" ) )
			{
				// enter all the available skus from the Play Developer Console in this array so that item information can be fetched for them
				var skus = new string[] { "com.prime31.testproduct", "android.test.purchased", "com.prime31.noads" };
				GoogleIAB.queryInventory( skus );
			}


			if( GUILayout.Button( "Get Purchase History" ) )
			{
				Debug.Log( "purchase history: " );
				Utils.logObject( GoogleIAB.getPurchaseHistory() );
			}


			if( GUILayout.Button( "Are subscriptions supported?" ) )
			{
				Debug.Log( "subscriptions supported: " + GoogleIAB.areSubscriptionsSupported() );
			}


			if( GUILayout.Button( "Purchase Test Product" ) )
			{
				GoogleIAB.purchaseProduct( "android.test.purchased" );
			}


			if( GUILayout.Button( "Consume Test Purchase" ) )
			{
				GoogleIAB.consumeProduct( "android.test.purchased" );
			}


			if( GUILayout.Button( "Acknowledge Test Purchase" ) )
			{
				GoogleIAB.acknowledgePurchase( "android.test.purchased" );
			}


			if( GUILayout.Button( "Purchase Unavailable Item" ) )
			{
				GoogleIAB.purchaseProduct( "android.test.item_unavailable" );
			}


			endColumn( true );


			if (skus != null && skus.Count > 0)
			{
				if (GUILayout.Button("Purchase Real Product"))
				{
					GoogleIAB.purchaseProduct(skus[0].productId);
				}


				if (GUILayout.Button("Consume Real Purchase"))
				{
					GoogleIAB.consumeProduct(skus[0].productId);
				}
			}
			else
			{
				GUILayout.Label("No Skus found in inventory. Query your inventory first.");
			}


			if( GUILayout.Button( "Enable High Details Logs" ) )
			{
				GoogleIAB.enableLogging( true );
			}

			endColumn();
		}
#endif
	}

}
