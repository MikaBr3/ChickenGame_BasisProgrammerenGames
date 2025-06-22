# Shop System Setup Guide

This guide will help you set up the shop system in your Unity game where players can walk to a shop location and purchase upgrades.

## Prerequisites
- Make sure your player GameObject has the "Player" tag
- Ensure you have a MoneyManager in your scene
- Make sure you have an Upgrades GameObject in your scene

## Step 1: Create the Shop Location

1. **Create a Shop GameObject:**
   - Create an empty GameObject in your scene
   - Name it "Shop"
   - Position it where you want the shop to be on your map

2. **Add Visual Representation:**
   - Create a child GameObject called "ShopIcon"image.png
   - Add a SpriteRenderer component
   - Assign a shop icon sprite (you can use the ShoppingCart.png from your Sprites folder)
   - Position it at the shop location

3. **Add Shop Trigger:**
   - Add a BoxCollider2D component to the Shop GameObject
   - Check "Is Trigger" in the BoxCollider2D settings
   - Adjust the size to create the interaction area
   - Add the `ShopTrigger` script to the Shop GameObject

## Step 2: Create the Shop UI

1. **Create Shop UI Canvas:**
   - Create a new Canvas (Right-click in Hierarchy → UI → Canvas)
   - Name it "ShopCanvas"
   - Set the Canvas to "Screen Space - Overlay"

2. **Create Shop Panel:**
   - Create a Panel as a child of the Canvas
   - Name it "ShopPanel"
   - Set the background color to a semi-transparent dark color
   - Make it cover the full screen

3. **Add Shop Content:**
   - Create a Panel as a child of ShopPanel called "ShopContent"
   - Add a Grid Layout Group component
   - Set it to display items in a grid format

4. **Create Upgrade Buttons:**
   For each upgrade (Damage, Speed, Fire Rate, Health):
   - Create a Button as a child of ShopContent
   - Name it "DamageButton", "SpeedButton", etc.
   - Add a TextMeshPro Text component to display upgrade info
   - Style the buttons as needed

5. **Add Close Button:**
   - Create a Button in the top-right corner of ShopPanel
   - Name it "CloseButton"
   - Add text "X" or "Close"

6. **Add Money Display:**
   - Create a TextMeshPro Text component in the top-left of ShopPanel
   - Name it "MoneyText"
   - Set text to "Money: $0"

## Step 3: Configure the Scripts

1. **Configure ShopTrigger:**
   - Select the Shop GameObject
   - In the ShopTrigger component:
     - Drag the ShopPanel to the "Shop UI" field
     - Set "Shop Name" to "Upgrade Shop"
     - Drag the ShopIcon to the "Shop Icon" field
     - Set "Interact Key" to "E" (or your preferred key)

2. **Configure ShopUI:**
   - Add the `ShopUI` script to the ShopPanel
   - Drag the corresponding buttons to the button fields
   - Drag the corresponding text components to the text fields
   - Drag the MoneyText to the "Money Text" field

3. **Configure Upgrades:**
   - Create an empty GameObject called "UpgradesManager"
   - Add the `Upgrades` script to it
   - The script will automatically set up the upgrade system

## Step 4: Test the System

1. **Start the game**
2. **Walk to the shop location** (you should see a message when you enter the area)
3. **Press E to open the shop**
4. **Try purchasing upgrades** (you'll need money first)
5. **Press Escape or click Close to exit**

## Step 5: Add Money to Test

To test the shop, you can add money by:
1. Opening the console (Window → General → Console)
2. Finding the MoneyManager in your scene
3. Calling `MoneyManager.Instance.AddMoney(100)` in the console

## Troubleshooting

- **Shop doesn't open:** Make sure the player has the "Player" tag
- **Upgrades don't work:** Ensure the UpgradesManager GameObject exists in the scene
- **Money doesn't update:** Check that MoneyManager is properly set up
- **Buttons don't respond:** Verify all UI references are assigned in the ShopUI script

## Customization

- **Change upgrade costs:** Modify the values in the Upgrades script
- **Add new upgrades:** Create new Upgrade objects in the Upgrades script
- **Change shop appearance:** Modify the UI elements in the ShopPanel
- **Add sound effects:** Add AudioSource components and play sounds on purchase

## Files Created/Modified

- `Assets/Scripts/UI/ShopTrigger.cs` - Detects player interaction with shop
- `Assets/Scripts/UI/ShopUI.cs` - Manages the shop interface
- `Assets/Scripts/UI/Upgrades.cs` - Handles upgrade system
- `Assets/MoneyManager.cs` - Added SpendMoney and GetCurrentMoney methods
- `Assets/Player/PlayerMovement.cs` - Added fireRate support
- `Assets/Player/PlayerHealth.cs` - Renamed class and added public methods
- `Assets/Bullet.cs` - Added damage upgrade support 