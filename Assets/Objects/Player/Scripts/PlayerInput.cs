using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[System.Serializable]
public class PlayerInput
{
	[SerializeField] private string horizontalName = "Horizontal";
	[SerializeField] private string verticalName = "Vertical";
	[SerializeField] private string actionName = "Action";
    [SerializeField] private string boostName = "Boost";
    [SerializeField] private string stompName = "Stomp";

    public float horizontal { get; private set; }
	public float vertical { get; private set; }

	public bool right { get; private set; }
	public bool left { get; private set; }
	public bool up { get; private set; }
	public bool down { get; private set; }

	public bool action { get; private set; }
	public bool actionDown { get; private set; }
	public bool actionUp { get; private set; }

    public bool boostAction { get; private set; }
    public bool boostActionDown { get; private set; }
    public bool boostActionUp { get; private set; }

    public bool stompAction { get; private set; }
    public bool stompActionDown { get; private set; }
    public bool stompActionUp { get; private set; }

    private bool controlLocked;
	private float unlockTimer;

	public void InputUpdate()
	{
		UpdateAxes();
		UpdateAction();
		UpdateBoost();
		UpdateStomp();
	}

	private void UpdateAxes()
	{
		horizontal = !controlLocked ? CrossPlatformInputManager.GetAxis(horizontalName) : 0;
		vertical = CrossPlatformInputManager.GetAxis(verticalName);
		actionDown = actionUp = false;
		right = horizontal > 0;
		left = horizontal < 0;
		up = vertical > 0;
		down = vertical < 0;
	}

	private void UpdateAction()
	{
		if (CrossPlatformInputManager.GetButton(actionName))
		{
			if (!action)
			{
				action = true;
				actionDown = true;
			}
        }
		else
		{
			if (action)
			{
				action = false;
				actionUp = true;
			}
        }
	}

    private void UpdateBoost()
    {
        if (CrossPlatformInputManager.GetButton(boostName))
        {
            if (!boostAction)
            {
                boostAction = true;
                boostActionDown = true;
            }
        }
        else
        {
            if (boostAction)
            {
                boostAction = false;
                boostActionUp = true;
            }
        }
    }

    private void UpdateStomp()
    {
        if (CrossPlatformInputManager.GetButton(stompName))
        {
            if (!stompAction)
            {
                stompAction = true;
                stompActionDown = true;
            }
        }
        else
        {
            if (stompAction)
            {
                stompAction = false;
                stompActionUp = true;
            }
        }
    }

    public void LockHorizontalControl(float time)
	{
		unlockTimer = time;
		controlLocked = true;
	}

	public void UnlockHorizontalControl(float deltaTime)
	{
		if (unlockTimer > 0)
		{
			unlockTimer -= deltaTime;

			if (unlockTimer <= 0)
			{
				unlockTimer = 0;
				controlLocked = false;
			}
		}
	}
}