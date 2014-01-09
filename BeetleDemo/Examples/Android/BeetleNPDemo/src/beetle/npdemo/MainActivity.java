package beetle.npdemo;

import java.util.Date;

import beetle.netpackage.INetClientHandler;
import beetle.netpackage.NetClient;
import android.os.Bundle;
import android.os.Handler;
import android.app.Activity;
import android.view.Menu;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class MainActivity extends Activity implements INetClientHandler {

	private NetClient mClient;

	private Exception mLastError;

	private EditText mName;

	private EditText mEMail;

	private EditText mCity;

	private EditText mCountry;

	private TextView mError;

	private TextView mConnectionStatus;

	private TextView mRegTime;

	private Handler mUIHandler;

	private Register mMSG_Register;
	
	private Button mRegister;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		mUIHandler = new Handler();
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		mName = (EditText) findViewById(R.id.txtName);
		mEMail = (EditText) findViewById(R.id.txtEMail);
		mCity = (EditText) findViewById(R.id.txtCity);
		mCountry = (EditText) findViewById(R.id.txtCountry);
		mError = (TextView) findViewById(R.id.txtError);
		mConnectionStatus = (TextView) findViewById(R.id.txtConnectionStatus);
		mRegister =(Button)findViewById(R.id.cmdRegister);
		mRegTime =(TextView)findViewById(R.id.txtRegTime);
		mClient = new NetClient("10.0.2.2", 9088, new NPPackage(), this);
		mClient.Connect();

	}

	public void OnRegister(View item)
	{
		Register reg = new Register();
		reg.City = mCity.getText().toString();
		reg.Country= mCountry.getText().toString();
		reg.Name = mName.getText().toString();
		reg.EMail = mEMail.getText().toString();
		reg.RegTime =new Date();
		
		mClient.Send(reg);
	}
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}

	@Override
	public void Receive(NetClient client, Object msg) {
		// TODO Auto-generated method stub
		if (msg.getClass().getSimpleName().equals("Register")) {
			mMSG_Register = (Register) msg;
			mUIHandler.post(new Runnable() {

				@Override
				public void run() {
					mRegTime.setText(mMSG_Register.RegTime.toString());

				}
			});
		}
	}

	@Override
	public void Error(NetClient client, Exception e) {
		// TODO Auto-generated method stub
		mLastError = e;
		mUIHandler.post(new Runnable() {

			@Override
			public void run() {
				mError.setText(mLastError.getMessage());

			}
		});
	}

	@Override
	public void Disposed(NetClient client) {
		// TODO Auto-generated method stub
		mUIHandler.post(new Runnable() {

			@Override
			public void run() {
				mConnectionStatus.setText("close!");

			}
		});
	}

	@Override
	public void Connected(NetClient client) {
		// TODO Auto-generated method stub
		mUIHandler.post(new Runnable() {

			@Override
			public void run() {
				mConnectionStatus.setText("connected!");

			}
		});
	}

}
