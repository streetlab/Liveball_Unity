#import "Tuby3.h"

static IOSMgr *_instance = [IOSMgr sharedInstance];

@implementation IOSMgr

@synthesize imagePicker;

+(IOSMgr *)sharedInstance{
    return _instance;
}

+(void)initialize{
    if(!_instance){
        _instance = [[IOSMgr alloc] init];
    }
}

-(id)init{
    self = [super init];
    if(!self)
        return nil;
    
    imagePicker = [[UIImagePickerController alloc] init];
    imagePicker.delegate = self;
    imagePicker.allowsEditing = YES;
    
    return self;
}

-(void)dealloc{
    [imagePicker release];
    imagePicker = nil;
    [super dealloc];
}

-(void)imagePickerController:(UIImagePickerController *)picker didFinishPickingMediaWithInfo:(NSDictionary *)info{
    UIImage *img = [info objectForKey:UIImagePickerControllerEditedImage];
    if(!img) img = [info objectForKey:UIImagePickerControllerOriginalImage];
    

    NSData* pickedData = UIImagePNGRepresentation(img);
    NSLog(@"length : %lu", (unsigned long)[pickedData length]);
    NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
    NSString *path=[[paths objectAtIndex:0] stringByAppendingPathComponent:@"temp.png"];
//    [pickedData getBytes:buffer length:[pickedData length]];
    [pickedData writeToFile:path atomically:YES];
//    handler([path cStringUsingEncoding:NSUTF8StringEncoding]);
    
    if ([[NSFileManager defaultManager] fileExistsAtPath:path]) {
        NSLog(@"file exists");
//        NSLog(@"length : %lu", (unsigned long)[pickedData length]);
    }
    
    UnitySendMessage("IOSMgr", "MsgReceived", [path cStringUsingEncoding:NSUTF8StringEncoding]);
    [UnityGetGLViewController() dismissViewControllerAnimated:YES completion:NULL];
}

-(void)imagePickerControllerDidCancel:(UIImagePickerController *)picker{
    [UnityGetGLViewController() dismissViewControllerAnimated:YES completion:NULL];
}

-(BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions{
    UIRemoteNotificationType notiType = UIRemoteNotificationTypeBadge |
    UIRemoteNotificationTypeAlert | UIRemoteNotificationTypeSound;
    
    [[UIApplication sharedApplication] registerForRemoteNotificationTypes:notiType];
    
    NSLog(@"didFinishLaunchingWithOptions");
    
    return YES;
}

-(void)application:(UIApplication *)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData *)deviceToken{
    NSLog(@"Success - DeviceToken : %@", deviceToken);
}



@end

extern "C"
void OpenGallery(const char* str){
    [IOSMgr sharedInstance].imagePicker.sourceType = UIImagePickerControllerSourceTypePhotoLibrary;
    [UnityGetGLViewController() presentViewController:[IOSMgr sharedInstance].imagePicker animated:YES completion:NULL];
}
