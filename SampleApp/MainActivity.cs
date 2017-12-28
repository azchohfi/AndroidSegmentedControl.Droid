using Android.App;
using Android.Graphics;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Info.Hoang8f.Android.Segmented;
using Fragment = Android.Support.V4.App.Fragment;

namespace SampleApp
{
    [Activity(Label = "SampleApp", MainLauncher = true, Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_sample);

            if (savedInstanceState == null)
            {
                SupportFragmentManager.BeginTransaction()
                        .Add(Resource.Id.container, new PlaceholderFragment())
                        .Commit();
            }
        }

        [Register("info.hoang8f.android.segmented.SampleActivity$PlaceholderFragment")]
        public class PlaceholderFragment : Fragment, RadioGroup.IOnCheckedChangeListener, View.IOnClickListener
        {
            SegmentedGroup _segmented5;

            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                View rootView = inflater.Inflate(Resource.Layout.fragment_sample, container, false);

                SegmentedGroup segmented2 = rootView.FindViewById<SegmentedGroup>(Resource.Id.segmented2);
                segmented2.SetTintColor(Color.DarkGray);
                
                SegmentedGroup segmented3 = rootView.FindViewById<SegmentedGroup>(Resource.Id.segmented3);
                //Tint color, and text color when checked
                segmented3.SetTintColor(Color.ParseColor("#FFD0FF3C"), Color.ParseColor("#FF7B07B2"));

                _segmented5 = rootView.FindViewById<SegmentedGroup>(Resource.Id.segmented5);
                Button addBtn = rootView.FindViewById<Button>(Resource.Id.add_segmented);
                Button removeBtn = rootView.FindViewById<Button>(Resource.Id.remove_segmented);

                //Set listener for button
                addBtn.SetOnClickListener(this);
                removeBtn.SetOnClickListener(this);

                //Set change listener on SegmentedGroup
                segmented2.SetOnCheckedChangeListener(this);
                segmented3.SetOnCheckedChangeListener(this);
                _segmented5.SetOnCheckedChangeListener(this);

                // Support awesome font
                AwesomeRadioButton button = rootView.FindViewById<AwesomeRadioButton>(Resource.Id.button42);
                button.SetMarkdownText("{fa_facebook} facebook");
                return rootView;
            }

            public void OnCheckedChanged(RadioGroup group, int checkedId)
            {
                switch (checkedId)
                {
                    case Resource.Id.button21:
                        Toast.MakeText(Activity, "One", ToastLength.Short).Show();
                        break;
                    case Resource.Id.button22:
                        Toast.MakeText(Activity, "Two", ToastLength.Short).Show();
                        break;
                    case Resource.Id.button31:
                        Toast.MakeText(Activity, "One", ToastLength.Short).Show();
                        break;
                    case Resource.Id.button32:
                        Toast.MakeText(Activity, "Two", ToastLength.Short).Show();
                        break;
                    case Resource.Id.button33:
                        Toast.MakeText(Activity, "Three", ToastLength.Short).Show();
                        break;
                }
            }

            public void OnClick(View v)
            {
                switch (v.Id)
                {
                    case Resource.Id.add_segmented:
                        AddButton(_segmented5);
                        break;
                    case Resource.Id.remove_segmented:
                        RemoveButton(_segmented5);
                        break;
                }
            }

            private void AddButton(SegmentedGroup group)
            {
                RadioButton radioButton = (RadioButton)Activity.LayoutInflater.Inflate(Resource.Layout.radio_button_item, null);
                radioButton.Text = "Button " + (group.ChildCount + 1);
                group.AddView(radioButton);
                group.UpdateBackground();
            }

            private void RemoveButton(SegmentedGroup group)
            {
                if (group.ChildCount < 1) return;
                group.RemoveViewAt(group.ChildCount - 1);
                group.UpdateBackground();

                //Update margin for last item
                if (group.ChildCount < 1) return;
                RadioGroup.LayoutParams layoutParams = new RadioGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                layoutParams.SetMargins(0, 0, 0, 0);
                group.GetChildAt(group.ChildCount - 1).LayoutParameters = layoutParams;
            }
        }
    }
}
