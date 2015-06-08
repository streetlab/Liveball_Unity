//
//  OrderedModel.h
//  JiverExample
//
//  Created by Jed Kyung on 2015. 4. 24..
//  Copyright (c) 2015년 JIVER.CO. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface OrderedModel : NSObject

- (id) init;

- (void) setPresent:(BOOL)tf;
- (BOOL) isPast;
- (BOOL) isPresent;

@end
