//
//  JiverError.h
//  JiverExample
//
//  Created by Jed Kyung on 2015. 4. 20..
//  Copyright (c) 2015년 JIVER.CO. All rights reserved.
//

#import <Foundation/Foundation.h>

#define kErrLogin 10000
#define kErrGetChannelInfo 11000
#define kErrFileUpload 12000
#define kErrLoadChannel 13000
#define kErrStartMessaging 14000
#define kErrJoinMessaging 14050
#define kErrEndMessaging 14100
#define kErrLoadMoreMessages 15000

@interface JiverError : NSObject

@end