/* tslint:disable */
/* eslint-disable */
/**
 * Users API
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: all
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import { exists, mapValues } from '../runtime';
/**
 * 
 * @export
 * @interface AddPermissionCommandDto
 */
export interface AddPermissionCommandDto {
    /**
     * 
     * @type {number}
     * @memberof AddPermissionCommandDto
     */
    userId: number;
    /**
     * 
     * @type {number}
     * @memberof AddPermissionCommandDto
     */
    roleId: number;
}

/**
 * Check if a given object implements the AddPermissionCommandDto interface.
 */
export function instanceOfAddPermissionCommandDto(value: object): boolean {
    let isInstance = true;
    isInstance = isInstance && "userId" in value;
    isInstance = isInstance && "roleId" in value;

    return isInstance;
}

export function AddPermissionCommandDtoFromJSON(json: any): AddPermissionCommandDto {
    return AddPermissionCommandDtoFromJSONTyped(json, false);
}

export function AddPermissionCommandDtoFromJSONTyped(json: any, ignoreDiscriminator: boolean): AddPermissionCommandDto {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'userId': json['userId'],
        'roleId': json['roleId'],
    };
}

export function AddPermissionCommandDtoToJSON(value?: AddPermissionCommandDto | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'userId': value.userId,
        'roleId': value.roleId,
    };
}

