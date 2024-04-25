export enum DefaultRoutePathes {
  NotSet = '',
  Profile = '/',
  LoginForms = '/loginForms',
  Error = '/error',
  Register='/register'
}

export enum DefaultRouteNames {
  NotSet = '',
  Profile = 'profile',
  LoginForms = 'loginForms',
  Error = 'error',
  Register='registerForms'
}
export class DefaultRouteSettings {
  public static LoginForms: RouteSettings = {
    Path: DefaultRoutePathes.LoginForms,
    Name: DefaultRouteNames.LoginForms
  }
  public static RegisterForms: RouteSettings = {
    Path: DefaultRoutePathes.Register,
    Name: DefaultRouteNames.Register
  }
  public static Error: RouteSettings = {
    Path: DefaultRoutePathes.Error,
    Name: DefaultRouteNames.Error
  }
  public static Profile: RouteSettings = {
    Path: DefaultRoutePathes.Profile,
    Name: DefaultRouteNames.Profile
  }
 
}
export class RouteSettings {
  Path: DefaultRoutePathes = DefaultRoutePathes.Error
  Name: DefaultRouteNames = DefaultRouteNames.NotSet
}
