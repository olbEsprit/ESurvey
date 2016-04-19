using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyManager.DAL.Abstract
{
    public interface IRepositoryHolder : IDisposable
    {
        IDeviceGroupRepository DeviceGroupRepository { get; }
        IDeviceInfoRepository DeviceInfoRepository { get; }
        IDeviceRepository DeviceRepository { get; }
        IExceptionLoggerDataRepository ExceptionLoggerDataRepository { get; }
        IRoomRepository RoomRepository { get; }
        IStabilizerRepository StabilizerRepository { get; }
        ISystemDeviceGroupsRepository SystemDeviceGroupsRepository { get; }
        ISystemDeviceInfoRepository SystemDeviceInfoRepository { get; }
        ISystemDevicesRepository SystemDevicesRepository { get; }

        IUserPremisionsRepository UserPremisionsRepository { get; }
        IUserRoomPremisionsRepository UserRoomPremisionsRepository { get; }
        IUsersRoomRepository UsersRoomRepository { get; }
        Task SaveChangesAsync();
        void SaveChanges();

    }
}
