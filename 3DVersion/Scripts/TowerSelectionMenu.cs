using Godot;
using System;

public partial class TowerSelectionMenu : Control
{
	[Export] public int radius = 100;
	private TextureButton closeButton;
	private bool isShowing;

    public override void _Ready() {
		closeButton = GetNode<TextureButton>("CloseButton");

		var buttons = GetChildren();
		int buttonIndex = 0;
		foreach (var child in buttons) {
			if (child is TextureButton button && button != closeButton) {
				ArrangeButton(button, buttonIndex);
				buttonIndex++;
			}
		}

		closeButton.Position = -closeButton.Size / 2;
		closeButton.Connect("pressed", new Callable(this, nameof(OnCloseButtonPressed)));
    }

	public bool getIsMenuShowing() {
		return isShowing;
	}

	public void setIsMenuShowing(bool updatedMenuShowing) {
		isShowing = updatedMenuShowing;
		if(isShowing) {
			Show();
		} else {
			Hide();
		}
	}

	private void ArrangeButton(TextureButton button, int index) {
		int buttonCount = GetButtonCount();
		float angle = index * 2 * Mathf.Pi / buttonCount;
		Vector2 targetPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius - button.Size / 2;
		button.Position = targetPosition;
		GD.Print(button.Name, angle, targetPosition, button.Position);

		switch (button.Name) {
			case "TurretButton":
				button.Connect("pressed", new Callable(this, nameof(OnPlaceTurretPressed)));
				break;
			case "ShieldButton":
				button.Connect("pressed", new Callable(this, nameof(OnPlaceShieldGeneratorPressed)));
				break;
			default:
				GD.Print($"Unknown button pressed: {button.Name}");
				break;
		}
	}

	private int GetButtonCount() {
		int count = 0;
		foreach(var child in GetChildren()) {
			if (child is TextureButton button && button != closeButton) {
				count ++;
			}
		}
		return count;
	}

	private void OnPlaceTurretPressed() {
		GD.Print("Place turret button pressed");
		setIsMenuShowing(false);
	}

	private void OnPlaceShieldGeneratorPressed() {
		GD.Print("Place Shield Generator button pressed");
		setIsMenuShowing(false);
	}

	private void OnCloseButtonPressed() {
		setIsMenuShowing(false);
	}
}
