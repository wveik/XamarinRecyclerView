using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using XamarinRecyclerView.Domain;
using System.Collections.Generic;

namespace XamarinRecyclerView {
    [Activity(Label = "XamarinRecyclerView", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity {

        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private RecyclerView.Adapter mAdapter;
        private MyList<Email> mEmails;

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            mEmails = new MyList<Email>();
            mEmails.Add(new Email() { Name = "tom", Subject = "Wanna hang out?", Message = "I'll be around tomorrow!!" });
            mEmails.Add(new Email() { Name = "tom", Subject = "Wanna hang out?", Message = "I'll be around tomorrow!!" });
            mEmails.Add(new Email() { Name = "tom", Subject = "Wanna hang out?", Message = "I'll be around tomorrow!!" });
            mEmails.Add(new Email() { Name = "tom", Subject = "Wanna hang out?", Message = "I'll be around tomorrow!!" });
            mEmails.Add(new Email() { Name = "tom", Subject = "Wanna hang out?", Message = "I'll be around tomorrow!!" });
            mEmails.Add(new Email() { Name = "tom", Subject = "Wanna hang out?", Message = "I'll be around tomorrow!!" });

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

            //Create out layout manager
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new RecyclerAdapter(mEmails, mRecyclerView);
            mEmails.Adapter = mAdapter;
            mRecyclerView.SetAdapter(mAdapter);

        }

        public override bool OnCreateOptionsMenu(IMenu menu) {
            MenuInflater.Inflate(Resource.Menu.actionbar, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item) {

            switch (item.ItemId) {

                case Resource.Id.add:
                    mEmails.Add(new Email() { Name = "tom", Subject = "Wanna hang out?", Message = "I'll be around tomorrow!!" });
                    return true;

                case Resource.Id.discard:
                    mEmails.Remove(mEmails.Count - 1);
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }

    public class MyList<T> {
        private List<T> mItems;
        private RecyclerView.Adapter mAdapter;

        public MyList() {
            mItems = new List<T>();
        }

        public RecyclerView.Adapter Adapter {
            get { return mAdapter; }
            set { mAdapter = value; }
        }

        public void Add(T item) {
            mItems.Add(item);

            if (Adapter != null) {
                Adapter.NotifyItemInserted(0);
            }

        }

        public void Remove(int position) {
            mItems.RemoveAt(position);

            if (Adapter != null) {
                Adapter.NotifyItemRemoved(0);
            }
        }

        public T this[int index] {
            get { return mItems[index]; }
            set { mItems[index] = value; }
        }

        public int Count {
            get { return mItems.Count; }
        }

    }

    public class RecyclerAdapter : RecyclerView.Adapter {
        private MyList<Email> mEmails;
        private RecyclerView mRecyclerView;

        public RecyclerAdapter(MyList<Email> emails, RecyclerView recyclerView) {
            mEmails = emails;
            mRecyclerView = recyclerView;
        }

        public class MyView : RecyclerView.ViewHolder {
            public View mMainView { get; set; }
            public TextView mName { get; set; }
            public TextView mSubject { get; set; }
            public TextView mMessage { get; set; }

            public MyView(View view)
                : base(view) {
                mMainView = view;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
            View row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.row, parent, false);

            TextView txtName = row.FindViewById<TextView>(Resource.Id.txtName);
            TextView txtSubject = row.FindViewById<TextView>(Resource.Id.txtSubject);
            TextView txtMessage = row.FindViewById<TextView>(Resource.Id.txtMessage);

            MyView view = new MyView(row) { mName = txtName, mSubject = txtSubject, mMessage = txtMessage };
            return view;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
            MyView myHolder = holder as MyView;
            int indexPosition = (mEmails.Count - 1) - position;
            myHolder.mMainView.Click += mMainView_Click;
            myHolder.mName.Text = mEmails[indexPosition].Name;
            myHolder.mSubject.Text = mEmails[indexPosition].Subject;
            myHolder.mMessage.Text = mEmails[indexPosition].Message;
        }

        void mMainView_Click(object sender, EventArgs e) {
            int position = mRecyclerView.GetChildPosition((View)sender);
            int indexPosition = (mEmails.Count - 1) - position;
            Console.WriteLine(mEmails[indexPosition].Name);
        }

        public override int ItemCount {
            get { return mEmails.Count; }
        }
    }
}