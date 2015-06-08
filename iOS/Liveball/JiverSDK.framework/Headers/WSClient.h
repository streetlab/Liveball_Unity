//
//  WSClient.h
//  JiverExample
//
//  Created by JIVER Developers on 2015. 3. 3. in San Francisco, CA.
//  Copyright (c) 2015년 JIVER.CO. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "SRWebSocket.h"
#import "Command.h"
#import "Jiver.h"

@interface WSClient : NSObject<SRWebSocketDelegate>

- (id) initWithHost:(NSString *)host;
- (void) setEventHandlerOpenBlock:(void (^)())open messageBlock:(void (^)(NSString *data))message closeBlock:(void (^)())close errorBlock:(void (^)())error;
- (void) disconnect;
- (void) connect;
- (void) sendData:(id)data;
- (void) sendCommand:(Command *)command;

@end
