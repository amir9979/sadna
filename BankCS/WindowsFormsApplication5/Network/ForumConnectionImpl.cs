using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DataTypes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;


namespace client.Network
{
    public class ForumConnectionImpl : ForumConnection
    {
        Object _monitorLock;

        Object _AvailableLock;

        Object _WaitingReplyLock;

        string _ip;
        int _port;

        TcpClient _server;

        bool _isWaitingMsg;

        bool _isAvailable;

        FuncMsgServer _response;

        List<FuncMsgServer> _orders;


        public ForumConnectionImpl(string ip, int port)
        {
            _monitorLock = new Object();
            _AvailableLock = new Object();
            _WaitingReplyLock = new Object();
            _orders = new List<FuncMsgServer>();

            _ip = ip;
            _port = port;
            _server = null;
            _isWaitingMsg = false;
            _isAvailable = true;
            _response = null;
            
        }

        public override void connect()
        {
            lock (_monitorLock)
            {
                disconnect();
                _server = new TcpClient();
                _server.Connect(_ip, _port);
                Thread proxyThread = new Thread(this.reader);
                proxyThread.Start();
            }
        }

        private void reinit()
        {

        }

        public override void disconnect()
        {

            lock (_monitorLock)
            {
                if (_server == null)
                    return;
                _server.Close();
                _server = null;

            }
        }

        public override bool isConnected()
        {
            lock (_monitorLock)
            {
                return (_server != null && _server.Connected);
            }
        }

        public override bool entry(string ForumName)
        {
           
                List<object> lst = new List<object>{ ForumName };
                return sendFuncMsg<bool>(FuncMsgClient.FuncType.entry, lst);
        }

        public override bool SetPolicy(int index, string ForumName)
        {
            List<object> lst = new List<object> { index, ForumName };
            return sendFuncMsg<bool>(FuncMsgClient.FuncType.SetPolicy, lst);
        }

        public override long Registration(string ForumName, string name, string pass, string mail, string fullname)
        {
            List<object> lst = new List<object> { ForumName, name, pass, mail, fullname };
            return sendFuncMsg<long>(FuncMsgClient.FuncType.Registration, lst);
        }

        public override bool login(string username, string pass)
        {
            List<object> lst = new List<object> { username, pass};
            return sendFuncMsg<bool>(FuncMsgClient.FuncType.login, lst);
        }

        public override void loggout()
        {
            List<object> lst = new List<object> {};
            sendFuncMsg(FuncMsgClient.FuncType.loggout, lst);
            return;
        }

        public override bool AddNewSubForum(string subject, MemberInfo moderator)
        {
            List<object> lst = new List<object> { subject, moderator};
            return sendFuncMsg<bool>(FuncMsgClient.FuncType.AddNewSubForum, lst);
        }

        public override List<SubForumInfo> WatchAllSubForum()
        {
            List<object> lst = new List<object> {};
            return sendFuncMsg<List<SubForumInfo>>(FuncMsgClient.FuncType.WatchAllSubForum, lst);
        }

        public override List<PostInfo> WatchAllThreads(SubForumInfo s)
        {
            List<object> lst = new List<object> {s };
            return sendFuncMsg<List<PostInfo>>(FuncMsgClient.FuncType.WatchAllThreads, lst);
        }

        public override List<PostInfo> WatchAllComments(PostInfo s)
        {
            List<object> lst = new List<object> { s };
            return sendFuncMsg<List<PostInfo>>(FuncMsgClient.FuncType.WatchAllComments, lst);
        }

        public override bool PublishNewThread(string msg, SubForumInfo s)
        {
            List<object> lst = new List<object> { msg,s };
            return sendFuncMsg<bool>(FuncMsgClient.FuncType.PublishNewThread, lst);
        }

        public override bool PublishCommentPost(string msg, PostInfo p)
        {
            List<object> lst = new List<object> { msg, p };
            return sendFuncMsg<bool>(FuncMsgClient.FuncType.PublishCommentPost, lst);
        }

        public override int checkHowMuchMemberType()
        {
            List<object> lst = new List<object> {};
            return sendFuncMsg<int>(FuncMsgClient.FuncType.checkHowMuchMemberType, lst);
        }

        public override bool addNewType(string newType)
        {
            List<object> lst = new List<object> {newType };
            return sendFuncMsg<bool>(FuncMsgClient.FuncType.addNewType, lst);
        }

        public override bool promoteMemberToAdmin(MemberInfo u)
        {
            List<object> lst = new List<object> { u };
            return sendFuncMsg<bool>(FuncMsgClient.FuncType.promoteMemberToAdmin, lst);
        }

        public override bool EmailConfirm(long ConfNumber)
        {
            List<object> lst = new List<object> { ConfNumber };
            return sendFuncMsg<bool>(FuncMsgClient.FuncType.EmailConfirm, lst);
        }

        public override bool deleteType(string newType)
        {
            List<object> lst = new List<object> { newType };
            return sendFuncMsg<bool>(FuncMsgClient.FuncType.deleteType, lst);
        }

        public override bool deletePost(PostInfo p)
        {
            List<object> lst = new List<object> { p };
            return sendFuncMsg<bool>(FuncMsgClient.FuncType.deletePost, lst);
        }

        public override bool SPlogin(string superusername, string superpass)
        {
            List<object> lst = new List<object> { superusername, superpass };
            return sendFuncMsg<bool>(FuncMsgClient.FuncType.SPlogin, lst);
        }

        public override List<ForumInfo> WatchAllForums()
        {
            List<object> lst = new List<object> {};
            return sendFuncMsg<List<ForumInfo>>(FuncMsgClient.FuncType.WatchAllForums, lst);
        }

		
		public override List<MemberInfo> WatchAllMembers(ForumInfo forumInfo)
        {
            List<object> lst = new List<object> { forumInfo };
            return sendFuncMsg<List<MemberInfo>>(FuncMsgClient.FuncType.promoteMemberToAdmin, lst);

        }

        public override bool BuildForum(string name)
        {
            List<object> lst = new List<object> {name };
            return sendFuncMsg<bool>(FuncMsgClient.FuncType.BuildForum, lst);
        }

        public override void CancelForum(ForumInfo f)
        {
            List<object> lst = new List<object> { f};
            sendFuncMsg(FuncMsgClient.FuncType.CancelForum, lst);
            return;
        }

        public override ForumInfo GetForumByName(string forum)
        {
            List<object> lst = new List<object> { forum };
            return sendFuncMsg<ForumInfo>(FuncMsgClient.FuncType.GetForumByName, lst);
        }




        private void sendMsg(FuncMsgClient.FuncType type, List<object> args)
        {
            FuncMsgClient msg = new FuncMsgClient();
            msg.type = type;
            msg.args = args;
            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Serialize(_server.GetStream(), msg);
        }

        private void waitMyTurn()
        {
            lock (_monitorLock)
            {
                    while(!_isAvailable)
                        Monitor.Wait(_monitorLock);
            }
        }

        private void waitResponse()
        {
            lock (_monitorLock)
            {
                    while (!_isWaitingMsg)
                        Monitor.Wait(_monitorLock);
                    _isWaitingMsg = false;
            }
        }

        private void setResponse(FuncMsgServer msg)
        {
            lock (_monitorLock)
            {
                    _response = msg;
                    _isWaitingMsg = true;
                    Monitor.PulseAll(_monitorLock);
            }
        }

        private void finishMyTurn()
        {
            lock (_monitorLock)
            {
                    _isAvailable = true;
                    Monitor.PulseAll(_monitorLock);
            }
        }

        private T sendFuncMsg<T>(FuncMsgClient.FuncType type, List<object> args)
        {
            lock (_monitorLock)
            {
                waitMyTurn();
                sendMsg(type, args);
                waitResponse();
                if (!argCheck<T>(_response.args, 0) || _response.type==FuncMsgServer.FuncType.ErrorReplay)
                    throw new BadResponseException();


                T arg = getArgument<T>(_response.args, 0);

                finishMyTurn();
                return arg;
            }
        }

        private void sendFuncMsg(FuncMsgClient.FuncType type, List<object> args)
        {
            lock (_monitorLock)
            {
                waitMyTurn();
                sendMsg(type, args);
                waitResponse();

                finishMyTurn();
                return;
            }

        }

        private T getArgument<T>(List<object> args, int Index)
        {
            return (T)args[Index];
        }

        private bool argCheck<T>(List<object> args, int Index)
        {
            return args!=null&&args.Count>Index&&args[Index] is T;
        }


        private void reader()
        {
            BinaryFormatter bformatter = new BinaryFormatter();
            while (true)
            {
                    FuncMsgServer msg = (FuncMsgServer)bformatter.Deserialize(_server.GetStream());
                    if (msg.type == FuncMsgServer.FuncType.Replay || msg.type == FuncMsgServer.FuncType.ErrorReplay)
                        setResponse(msg);
                    else
                        _orders.Add(msg);
                    /*
                catch
                {
                    return;
                }
                     * */
            }
        }
    }
}
