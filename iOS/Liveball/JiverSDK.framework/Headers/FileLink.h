//
//  FileLink.h
//  JiverExample
//
//  Created by JIVER Developers on 2015. 3. 2. in San Francisco, CA.
//  Copyright (c) 2015년 JIVER.CO. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "FileInfo.h"
#import "Sender.h"
#import "OrderedModel.h"

@interface FileLink : OrderedModel

@property (retain) Sender *sender;
@property long long timestamp;
@property (retain) FileInfo *fileInfo;
@property BOOL isOpMessage;
@property BOOL isGuestMessage;
@property (retain) NSDictionary *jsonObj;

- (id) initWithDic:(NSDictionary *)dic inPresent:(BOOL)present;
- (NSString *)getSenderName;
- (NSString *) toJson;

@end
