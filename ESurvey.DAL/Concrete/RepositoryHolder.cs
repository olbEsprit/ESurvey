using EnergyManager.DAL.Abstract;
using EnergyManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyManager.DAL.Concrate
{
    public class RepositoryHolder : IRepositoryHolder
    {
        protected EnergyManagerEntities _context; 
        protected IDeviceGroupRepository _deviceGroupRepository;
        protected IDeviceInfoRepository _deviceInfoRepository;
        protected IDeviceRepository _deviceRepository;
        protected IExceptionLoggerDataRepository _exceptionLoggerDataRepository;
        protected IRoomRepository _roomRepository;
        protected IStabilizerRepository _stabilizerRepository;
        protected ISystemDeviceGroupsRepository _systemDeviceGroupsRepository;
        protected ISystemDeviceInfoRepository _systemDeviceInfoRepository;
        protected ISystemDevicesRepository _systemDevicesRepository;
        protected IUserPremisionsRepository _userPremisionsRepository;
        protected IUserRoomPremisionsRepository _userRoomPremisionsRepository;
        protected IUsersRoomRepository _usersRoomRepository;

        public RepositoryHolder()
        {
            _context = new EnergyManagerEntities();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IDeviceGroupRepository DeviceGroupRepository
        {
            get 
            {
                if (_deviceGroupRepository == null)
                    _deviceGroupRepository = new DeviceGroupRepository(_context);
                return _deviceGroupRepository;
            }
        }

        public IDeviceInfoRepository DeviceInfoRepository
        {
            get
            {
                if (_deviceInfoRepository == null)
                    _deviceInfoRepository = new DeviceInfoRepository(_context);
                return _deviceInfoRepository;
            }
        }

        public IDeviceRepository DeviceRepository
        {
            get
            {
                if (_deviceRepository == null)
                    _deviceRepository = new DeviceRepository(_context);
                return _deviceRepository;
            }
        }

        public IExceptionLoggerDataRepository ExceptionLoggerDataRepository
        {
            get
            {
                if (_exceptionLoggerDataRepository == null)
                    _exceptionLoggerDataRepository = new ExceptionLoggerDataRepository(_context);
                return _exceptionLoggerDataRepository;
            }
        }

        public IRoomRepository RoomRepository
        {
            get
            {
                if (_roomRepository == null)
                    _roomRepository  = new RoomRepository(_context);
                return _roomRepository;
            }
        }

        public IStabilizerRepository StabilizerRepository
        {
            get
            {
                if (_stabilizerRepository == null)
                    _stabilizerRepository = new StabilizerRepository(_context);
                return _stabilizerRepository;
            }
        }

        public ISystemDeviceGroupsRepository SystemDeviceGroupsRepository
        {
            get
            {
                if (_systemDeviceGroupsRepository == null)
                    _systemDeviceGroupsRepository = new SystemDeviceGroupsRepository(_context);
                return _systemDeviceGroupsRepository;
            }
        }

        public ISystemDeviceInfoRepository SystemDeviceInfoRepository
        {
            get
            {
                if (_systemDeviceInfoRepository == null)
                    _systemDeviceInfoRepository = new SystemDeviceInfoRepository(_context);
                return _systemDeviceInfoRepository;
            }
        }

        public ISystemDevicesRepository SystemDevicesRepository
        {
            get
            {
                if (_systemDevicesRepository == null)
                    _systemDevicesRepository = new SystemDevicesRepository(_context);
                return _systemDevicesRepository;
            }
        }
        

        public IUserPremisionsRepository UserPremisionsRepository
        {
            get
            {
                if (_userPremisionsRepository == null)
                    _userPremisionsRepository = new UserPremisionsRepository(_context);
                return _userPremisionsRepository;
            }
        }

        public IUserRoomPremisionsRepository UserRoomPremisionsRepository
        {
            get
            {

                if (_userRoomPremisionsRepository == null)
                    _userRoomPremisionsRepository = new UserRoomPremisionsRepository(_context);
                return _userRoomPremisionsRepository;
            }
        }

        public IUsersRoomRepository UsersRoomRepository
        {
            get
            {
                if (_usersRoomRepository == null)
                    _usersRoomRepository = new UsersRoomRepository(_context);
                return _usersRoomRepository;
            }
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges(); 
        }
    }
}
