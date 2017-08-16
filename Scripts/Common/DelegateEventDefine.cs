using System.Collections;
using System.Collections.Generic;
using TaiDouCommon.Model;


public delegate void OnGetRolesEvent(List<Role> list);
public delegate void OnAddRoleEvent(Role role);
public delegate void OnSelectedRoleEvent();


public delegate void OnGetTaskDBsEvent(List<TaskDB> list);
public delegate void OnAddTaskDBEvent(TaskDB role);
public delegate void OnUpdateTaskDBEvent();

public delegate void OnSyncTaskCompletedEvent();

public delegate void OnGetInventoryItemDBsEvent(List<InventoryItemDB> list);
public delegate void OnAddInventoryItemDBEvent(InventoryItemDB list);
public delegate void OnUpdateInventoryItemDBEvent();